using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class GestureDetector : MonoBehaviour
{
    public static GestureDetector Instance;
    public GestureRecognizer GestureRecognizer { get; private set; }

    [SerializeField]
    private HoverMenu hoverMenu;

    public void Start()
    {
        Instance = this;

        GestureRecognizer = new GestureRecognizer();
        GestureRecognizer.SetRecognizableGestures(GestureSettings.Tap);
        GestureRecognizer.TappedEvent += Recognizer_TappedEvent;

        GestureRecognizer.StartCapturingGestures();
    }

    public void OnDestroy()
    {
        GestureRecognizer.TappedEvent -= Recognizer_TappedEvent;

        GestureRecognizer.StopCapturingGestures();
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        hoverMenu.ToggleMenu();
    }
}