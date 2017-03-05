using HoloToolkit.Unity.SpatialMapping;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GestureDetector : MonoBehaviour
{
    public static GestureDetector Instance;
    public GestureRecognizer GestureRecognizer { get; private set; }
    public TextMesh text;

    private AudioSource dictationAudio;
    private MicrophoneManager microphoneManager;

    public void Awake()
    {
        Instance = this;

        GestureRecognizer = new GestureRecognizer();
        GestureRecognizer.SetRecognizableGestures(GestureSettings.Hold);
        GestureRecognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        GestureRecognizer.HoldCompletedEvent += Recognizer_HoldCompletedEvent;

        GestureRecognizer.StartCapturingGestures();

        dictationAudio = gameObject.GetComponent<AudioSource>();
        microphoneManager = GetComponent<MicrophoneManager>();
    }

    void OnDestroy()
    {
        GestureRecognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;
        GestureRecognizer.HoldCompletedEvent -= Recognizer_HoldCompletedEvent;

        GestureRecognizer.StopCapturingGestures();
    }

    private void Recognizer_HoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        text.text = "hold started";
        dictationAudio.clip = microphoneManager.StartRecording();
    }

    private void Recognizer_HoldCompletedEvent(InteractionSourceKind source, Ray headRay)
    {
        text.text = "hold ended";
        microphoneManager.StopRecording();
    }
}