using HoloToolkit.Unity;
using UnityEngine;
using UnityEngine.UI;

public class HoverMenu : MonoBehaviour
{
    public ChatroomManager Chatroom;
    public TextToSpeechManager textSpeech;

    public void Update()
    {
        // Maintain distance between camera and menu
        Vector3 offset = transform.position - Camera.main.transform.position;
        offset /= offset.magnitude;
        transform.position = Camera.main.transform.position + offset * 5;
    }

    public void ToggleMenu()
    {
        if(gameObject.activeSelf)
        {
            // Check if button is tapped
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                Button button = hit.transform.gameObject.GetComponent<Button>();
                if(button != null)
                    button.onClick.Invoke();
            }
        }
        else
        {
            // Set menu position to directly in front of user
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        }


        gameObject.SetActive(!gameObject.activeSelf);
        Chatroom.gameObject.SetActive(!gameObject.activeSelf);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Chatroom.gameObject.SetActive(true);
    }

    public void RegisterNewSpeaker()
    {
        Debug.Log("New speaker");
    }

    public void EmitReply(string message)
    {
        Chatroom.AddMessage("Me", message);
        textSpeech.SpeakText(message);
    }
}
