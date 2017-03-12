using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class VoiceIdentification : MonoBehaviour {

    // API endpoints
    public static readonly string PROFILE_ENDPOINT = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles";
    public static readonly string IDENTIFY_ENDPOINT = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identify";

    // API key
    private static readonly string KEY = "c7c70ee76b594bfd9dc7fa0d016fc462";

    // Users ids that is used for the identification request
    Dictionary<string, string> userIds = new Dictionary<string, string>();

    public void Start()
    {
        StartCoroutine(GetAllUsersFromDB());
    }

    public void MakeNewUser(byte[] audio, string name)
    {
        // Create the user profile
        CreateProfileAPI(audio, name);
    }

    public void IdentifyUser(byte[] audio, Action<string> onUserIdentified)
    {
        IdentifyRequestAPI(audio, onUserIdentified);
    }

    #region API calls

    private void CreateProfileAPI(byte[] audio, string name)
    {
        // Create the user profile by calling the api
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c068e597ed22ec38f42e

        // Construct the headers
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        headers.Add("Ocp-Apim-Subscription-Key", KEY);

        // Define the payload
        string body = "{\"locale\":\"en-us\"}";

        // Call the api endpoint (POST)
        WWW www = new WWW(PROFILE_ENDPOINT, Encoding.ASCII.GetBytes(body.ToCharArray()), headers);
        StartCoroutine(CreateProfileRequest(www, audio, name, HandleCreateProfile));
    }

    private void EnrollUserAPI(string id, byte[] audio)
    {
        // Enroll the user with the audio file
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c3271984551c84ec6797

        // Convert the byte[] to a WAV file
        byte[] header = WAV.WriteWavHeader(false, 1, 16, 16000, audio.Length);
        byte[] all = new byte[header.Length + audio.Length];
        for (int i = 0; i < header.Length; i++)
        {
            all[i] = header[i];
        }
        for (int i = 0; i < audio.Length; i++)
        {
            all[i + header.Length] = audio[i];
        }

        // Construct the headers
        WWWForm form = new WWWForm();
        form.AddBinaryData("Data", all, "testFile_" + DateTime.Now.ToString("u"));
        Dictionary<string, string> headers = form.headers;
        headers.Add("Ocp-Apim-Subscription-Key", KEY);

        // Set the request parameters in the url
        // FIXME determine short audio based on length of sound file
        string url = PROFILE_ENDPOINT + "/" + id + "/enroll?shortAudio=true";
        Debug.Log("Enroll URL = " + url);

        // Call the api endpoint (POST)
        WWW www = new WWW(url, form.data, headers);
        StartCoroutine(EnrollUserRequest(www, HandleEnrollUser));
    }

    private void IdentifyRequestAPI(byte[] audio, Action<string> onUserIdentified)
    {
        // Create a Identification Request
        // https://dev.projectoxford.ai/docs/services/563309b6778daf02acc0a508/operations/5645c523778daf217c292592

        // Convert the byte[] to a WAV file
        byte[] header = WAV.WriteWavHeader(false, 1, 16, 16000, audio.Length);
        byte[] all = new byte[header.Length + audio.Length];
        for (int i = 0; i < header.Length; i++)
        {
            all[i] = header[i];
        }
        for (int i = 0; i < audio.Length; i++)
        {
            all[i + header.Length] = audio[i];
        }

        // Construct the headers
        WWWForm form = new WWWForm();
        form.AddBinaryData("Data", all, "testFile_" + DateTime.Now.ToString("u"));
        Dictionary<string, string> headers = form.headers;
        headers.Add("Ocp-Apim-Subscription-Key", KEY);

        // Set the request parameters in the url
        // FIXME determine short audio based on length of sound file
        string url = IDENTIFY_ENDPOINT + "?identificationProfileIds=";
        foreach (string uid in userIds.Keys)
        {
            url += uid + ",";
        }
        url = url.Substring(0, url.Length - 1);
        url += "&enroll?shortAudio=true";
        Debug.Log("Identify URL = " + url);

        // Call the api endpoint (POST)
        WWW www = new WWW(url, form.data, headers);
        StartCoroutine(IdentifyUserRequest(www, onUserIdentified, HandleIdentifyUser));
    }

    private IEnumerator QueryForIdentificationAPI(string url, Action<string> onUserIdentified)
    {
        string code = "failed";
        string uid = "";
        bool done = false;
        for (int i = 0; i < 10 && !done; i++)
        {
            // Wait a little bit of time before API calls
            yield return new WaitForSeconds(2.0f);

            // Query for the identify operation to finish
            // https://dev.projectoxford.ai/docs/services/563309b6778daf02acc0a508/operations/5645c725ca73070ee8845bd6

            // Construct the headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", KEY);

            // Call the api endpoint (GET)
            WWW www = new WWW(url, null, headers);
            yield return www;

            // Check the status
            string res = www.text;
            Debug.Log(www.text);
            string[] split = res.Split('\"');

            // Find the status code in the json
            code = split[3];
            if (code.Equals("succeeded") || code.Equals("failed"))
            {
                done = true;
                uid = split[17];
            }
        }

        if (code.Equals("succeeded"))
        {
            Debug.Log("Identified with uid = " + uid);

            // Find the name from the db
            string name = userIds[uid];

            onUserIdentified(name);
        }
    }

    private IEnumerator GetAllUsersFromDB()
    {
        // Call the api endpoint (GET)
        WWW www = new WWW("http://soundgram-server.azurewebsites.net/users");
        yield return www;

        // Get the users
        string res = www.text;
        Debug.Log(www.text);
        string[] split = res.Split('{');

        for (int i = 1; i < split.Length; i++)
        {
            string user = split[i];
            string[] split2 = user.Split('\"');
            string uid = split2[3];
            string name = split2[7];
            userIds[uid] = name;
            Debug.Log("Users added: " + uid + " = " + name);
        }
    }

    private IEnumerator AddUserToDB(string uid, string name)
    {
        String url = "http://soundgram-server.azurewebsites.net/usersAdd?uid=" + uid + "&name=" + name;

        // Call the api endpoint (GET)
        WWW www = new WWW(url);
        yield return www;

        Debug.Log("Added new user to db! " + uid + " = " + name);
    }

    #endregion

    #region API request coroutine

    private IEnumerator CreateProfileRequest(WWW www, byte[] audio, string name, Action<string, byte[], string> callback)
    {
        yield return www;

        if (www.error == null)
        {
            callback(www.text, audio, name);
        }
        else
        {
            Debug.Log("ERROR: " + www.error + "\n" + www.text);
        }
    }

    private IEnumerator EnrollUserRequest(WWW www, Action<Dictionary<string, string>> callback)
    {
        yield return www;

        if (www.error == null)
        {
            callback(www.responseHeaders);
        }
        else
        {
            Debug.Log("ERROR: " + www.error + "\n" + www.text);
        }
    }

    private IEnumerator IdentifyUserRequest(WWW www, Action<string> onUserIdentified, Action<Dictionary<string, string>, Action<string>> callback)
    {
        yield return www;

        if (www.error == null)
        {
            callback(www.responseHeaders, onUserIdentified);
        }
        else
        {
            Debug.Log("ERROR: " + www.error + "\n" + www.text);
        }
    }

    #endregion

    #region API response handlers

    private void HandleCreateProfile(string res, byte[] audio, string name)
    {
        Debug.Log(res);

        // FIXME figure out how to do real json parsing
        string[] split = res.Split('\"');
        string id = split[3];
        userIds[id] = name;
        Debug.Log("Created user profile " + id + " with name " + name);

        // Add user to db
        StartCoroutine(AddUserToDB(id, name));

        // We have a new id for the user profile now... enroll the new user
        EnrollUserAPI(id, audio);
    }

    private void HandleEnrollUser(Dictionary<string, string> responseHeaders)
    {
        Debug.Log("Enrolled at " + responseHeaders["Operation-Location"]);
    }

    private void HandleIdentifyUser(Dictionary<string, string> responseHeaders, Action<string> onUserIdentified)
    {
        string url = responseHeaders["Operation-Location"];
        Debug.Log("Identifying at " + url);

        StartCoroutine(QueryForIdentificationAPI(url, onUserIdentified));
    }

    #endregion
}
