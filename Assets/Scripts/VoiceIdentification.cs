using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Net;

public class VoiceIdentification : MonoBehaviour {

    // TODO storing the key in not plaintext!
    private string key = "KEY GOES HERE";

    public VoiceIdentification()
    {
        
    }

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

    public void EnrollUser(string id, Stream audioStream)
    {
        // TODO Create the user profile

        // TODO Retrieve the created profile and store it
        Debug.Log("Created user profile");

        // TODO Call the enroll API
        Debug.Log("Enrolling " + id);

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
}
