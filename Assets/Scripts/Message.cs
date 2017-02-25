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

    public void SetMessage(string speaker, string text)
    {
        this.speaker = speaker;
        this.text = text;
        GetComponent<Text>().text = "<b>" + speaker + "</b>: " + text;
    }
}
