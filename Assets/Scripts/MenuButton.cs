using UnityEngine;
using UnityEngine.UI;

public enum ButtonType { Register, Navigation, Reply, Close }

public class MenuButton : MonoBehaviour
{
    public ButtonType Type;
    public int DestinationMenu; // Used for navigation buttons
    public string Message; // Used for reply buttons

    private Image image;
    private Text text;

    private Vector3 initialScale;

    private Vector3 targetPosition;
    private float targetScale;
    private float targetImageAlpha;
    private float targetTextAlpha;

    private Color imageColor;
    private Color textColor;

    public void Awake()
    {
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();

        initialScale = transform.localScale;
        targetPosition = transform.localPosition;
        targetScale = 1;
        targetImageAlpha = targetTextAlpha = 1;
        imageColor = textColor = new Color(1, 1, 1, 0);
    }

    public void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.1f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, initialScale * targetScale, targetScale / 10);

        imageColor = image.color;
        imageColor.a = Mathf.MoveTowards(imageColor.a, targetImageAlpha, 0.1f);
        image.color = imageColor;

        textColor = text.color;
        textColor.a = Mathf.MoveTowards(textColor.a, targetTextAlpha, 0.1f);
        text.color = textColor;
    }

    public void Appear(string displayText, string voiceMessage)
    {
        text.text = displayText;
        Message = voiceMessage;
    }

    public void Disappear()
    {

    }

    public void Replace(string displayText, string voiceMessage)
    {
        text.text = displayText;
        Message = voiceMessage;
    }
}
