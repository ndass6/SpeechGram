using HoloToolkit.Unity;
using System.Collections;
using UnityEngine;

public class HoverMenu : MonoBehaviour
{
    public MicrophoneManager microphone;
    public TextToSpeechManager textSpeech;
    public ChatroomManager Chatroom;

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
                MenuButton button = hit.transform.gameObject.GetComponent<MenuButton>();
                if(button != null)
                {
                    switch(button.Type)
                    {
                        case ButtonType.Register:
                            microphone.registeringNewUser = true;
                            break;
                        case ButtonType.Reply:
                            Chatroom.AddMessage("Me", button.Message);
                            textSpeech.SpeakText(button.Message);
                            break;
                    }
                    gameObject.SetActive(false);
                    Chatroom.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // Set menu position to directly in front of user
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;

            gameObject.SetActive(true);
            Chatroom.gameObject.SetActive(false);
        }
    }

    #region Animation coroutines

    private IEnumerator Appear()
    {
        yield return null;
    }

    private IEnumerator Disappear()
    {
        yield return null;
    }

    private IEnumerator Replace()
    {
        yield return null;
    }

    #endregion
}
