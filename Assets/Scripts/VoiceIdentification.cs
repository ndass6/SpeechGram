using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class VoiceIdentification : MonoBehaviour {

    // API endpoints
    public static readonly string PROFILE_ENDPOINT = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles";

    // FIXME storing the key in not plaintext!
    private static readonly string KEY = "c7c70ee76b594bfd9dc7fa0d016fc462";

    void Start () {
		// DEBUG on start
	}
	
	void Update () {
        // DEBUG on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    public void MakeNewUser(byte[] audio)
    {
        // Create the user profile
        CreateProfile(audio);
    }

    public void IdentifyUser(byte[] audio, Action<string> callback)
    {
        // TODO Call the Identify API
        
        // TODO Return to the caller, call the callback when we get a result
        
        // TODO verify the enrollement
        Debug.Log("Identification done");

        // TODO call the callback
    }

    #region API calls

    private void CreateProfile(byte[] audio)
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

    private void EnrollUser(string id, byte[] audio)
    {
        // Enroll the user with the audio file
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c3271984551c84ec6797

        // Boundary for the multipart/form-data
        string boundary = "Upload----" + DateTime.Now.ToString("u");

        // Construct the headers
        WWWForm form = new WWWForm();
        form.AddBinaryData("Data", audio, "testFile_" + DateTime.Now.ToString("u"));
        Dictionary<string, string> headers = form.headers;
        // headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
        headers.Add("Content-Type", "multipart/form-data");
        headers.Add("Ocp-Apim-Subscription-Key", KEY);

        // Set the request parameters in the url
        // FIXME determine short audio based on length of sound file
        string url = PROFILE_ENDPOINT + "/" + id + "/enroll?shortAudio=true";

        // TODO Set audio up in the request body
        string body = "TODO";

        // Call the api endpoint (POST)
        WWW www = new WWW(url, form);
        StartCoroutine(EnrollUserRequest(www, HandleEnrollUser));
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

    private IEnumerator EnrollUserRequest(WWW www, Action<string> callback)
    {
        yield return www;

        if (www.error == null)
        {
            callback(www.text);
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
        // FIXME figure out how to do real json parsing :(
        string id = res.Substring(34, 36);
        Debug.Log("Created user profile " + id);

        // We have a new id for the user profile now... enroll the new user
        EnrollUser(id, audio);
    }

    private void HandleEnrollUser(string response)
    {
        Debug.Log("Enrolling");
        Debug.Log(response);

        // TODO parse xml for the query endpoint

        // TODO verify enrollement
    }

    #endregion
}
