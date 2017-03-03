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

        // TODO Retrieve the created profile and store it
        Debug.Log("Created user profile");

        // TODO Call the enroll API
        Debug.Log("Enrolling");

        // TODO Return to the original caller while continuing this call to verify
        
        // TODO verify enrollement
        Debug.Log("Enrollment done");
    }

    public void IdentifyUser(Stream audioStream, Action<string> callback)
    {
        // TODO Call the Identify API
        
        // TODO Return to the caller, call the callback when we get a result
        
        // TODO verify the enrollement
        Debug.Log("Identification done");

        // TODO call the callback
    }

    #region API Methods

    private void CreateProfile(Stream audioStream)
    {
        // Create the user profile by calling the api
        // https://westus.dev.cognitive.microsoft.com/docs/services/563309b6778daf02acc0a508/operations/5645c068e597ed22ec38f42e

        // Construct the www form
        WWWForm form = new WWWForm();

        // Call the api endpoint
        WWW www = new WWW(PROFILE_ENDPOINT, form);
        StartCoroutine(CreateProfileRequest(www, audioStream, HandleCreateProfile));
    }

    #endregion

    #region API response handlers

    private void HandleCreateProfile(string res, Stream audioStream)
    {
        // TODO figure out how to do real json parsing
        string id = res.Substring(34, 36);
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

    #endregion
}
