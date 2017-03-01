using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.ProjectOxford.SpeakerRecognition;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Identification;
using System.IO;
using System;
using System.Net;

public class VoiceIdentification : MonoBehaviour {

    private SpeakerIdentificationServiceClient serviceClient;
    public Dictionary<string, Profile> newProfiles;

    public VoiceIdentification()
    {
        serviceClient = new SpeakerIdentificationServiceClient("KEY_GOES_HERE");
        // How do I not store my key in plain text in unity?!?!?!

        newProfiles = new Dictionary<string, Profile>();
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

    public void CreateUser(string id)
    {
        // Create the user profile
        CreateProfileResponse creationResponse = await serviceClient.CreateProfileAsync("en-us");

        // Retrieve the created profile and store it
        Profile profile = await serviceClient.GetProfileAsync(creationResponse.ProfileId);
        newProfiles.Add(id, profile);

        Debug.Log("Created user profile");
    }

    public void EnrollUser(string id, Stream audioStream)
    {
        // Call the enroll API
        OperationLocation processPollingLocation = await serviceClient.EnrollAsync(audioStream, newProfiles[id].ProfileId, true);

        Console.Log("Enrolling " + id);

        // Return to the original caller while continuing this call to verify
        yield break;
        
        EnrollmentOperation enrollmentResult;
        int numOfRetries = 10;
        TimeSpan timeBetweenRetries = TimeSpan.FromSeconds(5.0);
        while (numOfRetries > 0)
        {
            await Task.Delay(timeBetweenRetries);
            enrollmentResult = await _serviceClient.CheckEnrollmentStatusAsync(processPollingLocation);

            if (enrollmentResult.Status == Status.Succeeded)
            {
                break;
            }
            else if (enrollmentResult.Status == Status.Failed)
            {
                throw new EnrollmentException(enrollmentResult.Message);
            }
            numOfRetries--;
        }
        if (numOfRetries <= 0)
        {
            throw new EnrollmentException("Enrollment operation timeout.");
        }

        Console.Log("Enrollment done");
    }

    public void IdentifyUser(Stream audioStream, Action<string> callback)
    {
        // Call the Identify API
        OperationLocation processPollingLocation = await _serviceClient.IdentifyAsync(audioStream, testProfileIds, true);

        // Return to the caller, call the callback when we get a result
        yield break;

        IdentificationOperation identificationResponse = null;
        int numOfRetries = 10;
        TimeSpan timeBetweenRetries = TimeSpan.FromSeconds(5.0);
        while (numOfRetries > 0)
        {
            await Task.Delay(timeBetweenRetries);
            identificationResponse = await _serviceClient.CheckIdentificationStatusAsync(processPollingLocation);

            if (identificationResponse.Status == Status.Succeeded)
            {
                break;
            }
            else if (identificationResponse.Status == Status.Failed)
            {
                throw new IdentificationException(identificationResponse.Message);
            }
            numOfRetries--;
        }
        if (numOfRetries <= 0)
        {
            throw new IdentificationException("Identification operation timeout.");
        }

        Console.Log("Identification done");

        // TODO call the callback
    }
}
