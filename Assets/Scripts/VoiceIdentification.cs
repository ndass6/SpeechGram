using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ProjectOxford.SpeakerRecognition;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification;

public class VoiceIdentification : MonoBehaviour {

    private SpeakerIdentificationServiceClient serviceClient;

    public VoiceIdentification()
    {
        serviceClient = new SpeakerIdentificationServiceClient("KEY_GOES_HERE");
        // How do I not store my key in plain text in unity?!?!?!
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
}
