using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class MicrophoneManager : MonoBehaviour
{
    public ChatroomManager chatroomManager;
    public VoiceIdentification identifier;

    [HideInInspector]
    public bool registeringNewUser = false;

    private DictationRecognizer dictationRecognizer;

    // Use this string to cache the text currently displayed in the text box.
    private StringBuilder textSoFar;

    // Using an empty string specifies the default microphone. 
    private static string deviceName = string.Empty;
    private int samplingRate;
    private const int messageLength = 30;

    private AudioSource dictationAudio;
    private bool converted;
    private float[] samples;

    private static int personCounter = 0;

    public void Start()
    {
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.AutoSilenceTimeoutSeconds = 3;
        dictationRecognizer.InitialSilenceTimeoutSeconds = 5;

        // Fires while the user is talking. As the recognizer listens, it provides text of what it's heard so far.
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;

        // Fires after the user pauses, typically at the end of a sentence. The full recognized string is returned here.
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;

        // Fires when the recognizer stops, whether from Stop() being called, a timeout occurring, or some other error.
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;

        // Fires when an error occurs
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;

        // Query the maximum frequency of the default microphone. Use 'unused' to ignore the minimum frequency.
        int unused;
        Microphone.GetDeviceCaps(deviceName, out unused, out samplingRate);

        // Use this string to cache the text currently displayed in the text box.
        textSoFar = new StringBuilder();

        dictationAudio = GetComponent<AudioSource>();
        
        dictationAudio.clip = StartRecording();
    }

    // Turns on the dictation recognizer and begins recording audio from the default microphone.
    public AudioClip StartRecording()
    {
        PhraseRecognitionSystem.Shutdown();
        dictationRecognizer.Start();
        return Microphone.Start(deviceName, false, messageLength, samplingRate);
    }

    // Ends the recording session.
    public void StopRecording()
    {
        if (dictationRecognizer.Status == SpeechSystemStatus.Running)
        {
            dictationRecognizer.Stop();
        }
        Microphone.End(deviceName);
    }

    // This event is fired while the user is talking. As the recognizer listens, it provides text of what it's heard so far.
    // text is the currently hypothesized recognition.
    private void DictationRecognizer_DictationHypothesis(string text) { }

    // This event is fired after the user pauses, typically at the end of a sentence. The full recognized string is returned here.
    // text is what was heard by the recognizer.
    // confidence is a representation of how confident (rejected, low, medium, high) the recognizer is of this recognition.
    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        textSoFar.Append(text + ". ");
    }

    // This event is fired when the recognizer stops, whether from Stop() being called, a timeout occurring, or some other error.
    // Typically, this will simply return "Complete". In this case, we check to see if the recognizer timed out.
    // cause is an enumerated reason for the session completing.</param>
    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        // If Timeout occurs, the user has been silent for too long.
        // With dictation, the default timeout after a recognition is 20 seconds.
        // The default timeout with initial silence is 5 seconds.
        if (cause == DictationCompletionCause.TimeoutExceeded)
        {
            Microphone.End(deviceName);
        }

        ConvertAudioClip(textSoFar.ToString());

        textSoFar = new StringBuilder();

        dictationAudio.clip = StartRecording();
    }

    private void ConvertAudioClip(string text)
    {
        // Convert AudioSource into byte array
        samples = new float[dictationAudio.clip.samples * dictationAudio.clip.channels];
        dictationAudio.clip.GetData(samples, 0);

        int start = 0;
        int end = samples.Length;
        for (int i = 0; i < samples.Length; i++)
        {
            if (samples[i] != 0)
            {
                start = i;
                break;
            }
        }
        for (int i = samples.Length - 1; i >= 0; i--)
        {
            if (samples[i] != 0)
            {
                end = i;
                break;
            }
        }

        for (int i = start; i < end; i++)
        {
            samples[i] = samples[i] * 0.5F;
            ++i;
        }

        var byteArray = new byte[(end - start) * 4];
        Buffer.BlockCopy(samples, start, byteArray, 0, (end - start));

        if (registeringNewUser)
        {
            personCounter++;
            string name = "Person" + personCounter;
            identifier.MakeNewUser(byteArray, name, text);
            registeringNewUser = false;
            ProcessUserIdentification(name, text);
        }
        else
        {
            identifier.IdentifyUser(byteArray, text, ProcessUserIdentification);
        }
    }

    // This event is fired when an error occurs.
    // error is the string representation of the error reason.</param>
    // hresult is the int representation of the hresult.</param>
    private void DictationRecognizer_DictationError(string error, int hresult) { }

    private void ProcessUserIdentification(string name, string text)
    {
        chatroomManager.AddMessage(name, text);
    }
}