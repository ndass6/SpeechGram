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
    HashSet<string> userIds = new HashSet<string>(); 

    public void MakeNewUser(byte[] audio)
    {
        // Create the user profile
        CreateProfileAPI(audio);
    }

    public void IdentifyUser(byte[] audio, Action<string> onUserIdentified)
    {
        IdentifyRequestAPI(audio, onUserIdentified);
    }

    #region API calls

    private void CreateProfileAPI(byte[] audio)
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
        StartCoroutine(CreateProfileRequest(www, audio, HandleCreateProfile));
    }

    private void EnrollUserAPI(string id, byte[] audio)
    {
        // Enroll the user with the audio file
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c3271984551c84ec6797

        // Convert the byte[] to a WAV file
        MemoryStream stream = new MemoryStream();
        WAV.WriteWavHeader(stream, false, 1, 16, 16000, audio.Length);
        stream.Write(audio, 0, audio.Length);

        // Construct the headers
        WWWForm form = new WWWForm();
        form.AddBinaryData("Data", stream.GetBuffer(), "testFile_" + DateTime.Now.ToString("u"));
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
        MemoryStream stream = new MemoryStream();
        WAV.WriteWavHeader(stream, false, 1, 16, 16000, audio.Length);
        stream.Write(audio, 0, audio.Length);

        // Construct the headers
        WWWForm form = new WWWForm();
        form.AddBinaryData("Data", stream.GetBuffer(), "testFile_" + DateTime.Now.ToString("u"));
        Dictionary<string, string> headers = form.headers;
        headers.Add("Ocp-Apim-Subscription-Key", KEY);

        // Set the request parameters in the url
        // FIXME determine short audio based on length of sound file
        string url = IDENTIFY_ENDPOINT + "?identificationProfileIds=";
        foreach (string uid in userIds)
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

    private IEnumerator QueryForIdentificationAPI(String url, Action<string> onUserIdentified)
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
            onUserIdentified(uid);
        }
    }

    #endregion

    #region API request coroutine

    private IEnumerator CreateProfileRequest(WWW www, byte[] audio, Action<string, byte[]> callback)
    {
        yield return www;

        if (www.error == null)
        {
            callback(www.text, audio);
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

    private void HandleCreateProfile(string res, byte[] audio)
    {
        Debug.Log(res);

        // FIXME figure out how to do real json parsing
        string[] split = res.Split('\"');
        string id = split[3];
        userIds.Add(id);
        Debug.Log("Created user profile " + id);

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
