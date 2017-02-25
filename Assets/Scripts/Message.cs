using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    private string speaker;
    public string Speaker
    {
        get { return speaker; }
    }
    private string text;
    public string Text
    {
        get { return text; }
    }

    private Text textComponent;

    public void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void Update()
    {
        if(textComponent.color.a < 1)
        {
            Color temp = textComponent.color;
            temp.a += Time.deltaTime;
            textComponent.color = temp;
        }
    }

    public void SetMessage(string speaker, string text)
    {
        speaker = speaker.Trim();
        text = text.Trim();
        this.speaker = speaker;
        this.text = text;
        GetComponent<Text>().text = "<b>" + speaker + "</b>: " + text;
    }
}
