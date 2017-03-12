using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class GestureDetector : MonoBehaviour
{
    public static GestureDetector Instance;
    public GestureRecognizer GestureRecognizer { get; private set; }

    [SerializeField]
    private HoverMenu hoverMenu;

    public void Awake()
    {
        Instance = this;

        GestureRecognizer = new GestureRecognizer();
        GestureRecognizer.SetRecognizableGestures(GestureSettings.Hold);
        GestureRecognizer.HoldStartedEvent += Recognizer_HoldStartedEvent;
        GestureRecognizer.HoldCompletedEvent += Recognizer_HoldCompletedEvent;

        GestureRecognizer.StartCapturingGestures();
    }

    public void OnDestroy()
    {
        GestureRecognizer.HoldStartedEvent -= Recognizer_HoldStartedEvent;
        GestureRecognizer.HoldCompletedEvent -= Recognizer_HoldCompletedEvent;

        GestureRecognizer.StopCapturingGestures();
    }

    private void Recognizer_HoldStartedEvent(InteractionSourceKind source, Ray headRay)
    {
        hoverMenu.OpenMenu();
    }

    private void Recognizer_HoldCompletedEvent(InteractionSourceKind source, Ray headRay)
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Button button = hit.transform.gameObject.GetComponent<Button>();
            if(button != null)
                button.onClick.Invoke();
        }
        hoverMenu.CloseMenu();
    }
}