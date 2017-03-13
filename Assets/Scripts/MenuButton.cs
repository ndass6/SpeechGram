using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public static readonly int NUM_BUTTONS = 12;

    public ButtonData Data;
    public bool Active;

    private Image image;
    private Text text;

    private Vector3 initialPosition;
    private Vector3 initialScale;

    private Vector3 targetPosition;
    private float targetScale;
    private float targetImageAlpha;
    private float targetTextAlpha;

    private Color imageColor;
    private Color textColor;

    // Replacing animation in progress
    private bool replacing;

    public void Awake()
    {
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();

        initialPosition = transform.localPosition;
        initialScale = transform.localScale;
        targetPosition = initialPosition;
        targetScale = 1;
        targetImageAlpha = targetTextAlpha = 1;
        imageColor = textColor = new Color(1, 1, 1, 0);

        replacing = false;
    }

    public void FixedUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, 0.05f);
        transform.localScale = Vector3.MoveTowards(transform.localScale, initialScale * targetScale, targetScale / 20);

        imageColor = image.color;
        imageColor.a = Mathf.MoveTowards(imageColor.a, targetImageAlpha, 0.05f);
        image.color = imageColor;

        textColor = text.color;
        textColor.a = Mathf.MoveTowards(textColor.a, targetTextAlpha, 0.05f);
        text.color = textColor;

        // Animation in progress
        if(!Active)
        {
            // Appear / replace animation finished
            if(text.color.a > 0.99f)
            {
                Active = true;
            }
            // Replace animation half-finished
            else if(replacing && text.color.a < 0.01f)
            {
                text.text = Data.Text;
                targetTextAlpha = 1;
                replacing = false;
            }
        }
    }

    public void Appear(ButtonData data)
    {
        Data = data;
        Active = false;

        transform.localPosition = initialPosition / 2;
        targetPosition = initialPosition;
        targetImageAlpha = 1;
        targetTextAlpha = 1;
        image.color = new Color(1, 1, 1, 0);
        text.color = new Color(1, 1, 1, 0);
        text.text = data.Text;
    }

    public void Disappear()
    {
        Active = false;

        transform.localPosition = initialPosition;
        targetPosition = initialPosition * 2;
        targetImageAlpha = 0;
        targetTextAlpha = 0;
    }

    public void Replace(ButtonData data)
    {
        Data = data;
        Active = false;
        
        targetTextAlpha = 0;

        replacing = true;
    }
}