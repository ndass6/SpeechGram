using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net;

public class VoiceIdentification : MonoBehaviour {

    // API endpoints
    public static readonly string PROFILE_ENDPOINT = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles";

    // TODO storing the key in not plaintext!
    private string key = "KEY GOES HERE";

    void Start () {
		// DEBUG on start
	}
	
	void Update () {
        // DEBUG on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // TODO
        }
    }

    public void EnrollUser(Stream audioStream)
    {
        // TODO Create the user profile
        CreateProfile(audioStream);
    }

    public void IdentifyUser(Stream audioStream, Action<string> callback)
    {
        // TODO Call the Identify API
        
        // TODO Return to the caller, call the callback when we get a result
        
        // TODO verify the enrollement
        Debug.Log("Identification done");

        // TODO call the callback
    }

    #region API calls

    private void CreateProfile(Stream audioStream)
    {
        // Create the user profile by calling the api
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c068e597ed22ec38f42e

        // Construct the www form
        WWWForm form = new WWWForm();
        // TODO add headers

        // Call the api endpoint
        WWW www = new WWW(PROFILE_ENDPOINT, form);
        StartCoroutine(CreateProfileRequest(www, audioStream, HandleCreateProfile));
    }

    private void EnrollUser(string id, Stream audioStream)
    {
        // Enroll the user with the audio file
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c3271984551c84ec6797

        // Construct the www form
        WWWForm form = new WWWForm();
        // TODO add headers

        // Call the api endpoint
        // TODO set parameters
        WWW www = new WWW(PROFILE_ENDPOINT, form);
        StartCoroutine(EnrollUserRequest(www, HandleEnrollUser));
    }

    #endregion

    #region API request coroutine

    private IEnumerator CreateProfileRequest(WWW www, Stream audioStream, Action<string, Stream> callback)
    {
        yield return www;

        if (www.error == null)
        {
            callback(www.text, audioStream);
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

    private void HandleCreateProfile(string res, Stream audioStream)
    {
        Debug.Log("Created user profile");

        // TODO figure out how to do real json parsing :(
        string id = res.Substring(34, 36);

        // We have a new id for the user profile now... enroll the new user
        EnrollUser(id, audioStream);
    }

    private void HandleEnrollUser(string response)
    {
        Debug.Log("Enrolling");

        // TODO parse xml for the query endpoint

        // TODO verify enrollement
    }

    #endregion
}
