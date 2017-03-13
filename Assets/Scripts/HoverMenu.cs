using UnityEngine;

public class HoverMenu : MonoBehaviour
{
    public ChatroomManager Chatroom;

    public void Update()
    {
        // Maintain distance between camera and menu
        Vector3 offset = transform.position - Camera.main.transform.position;
        offset /= offset.magnitude;
        transform.position = Camera.main.transform.position + offset * 5;
    }

    public void OpenMenu()
    {
        // Set menu position to directly in front of user
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;

        gameObject.SetActive(true);
        Chatroom.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        Chatroom.gameObject.SetActive(true);
    }

    public void RegisterNewSpeaker()
    {
        Debug.Log("New speaker");
        // TODO set to true
    }

    public void EmitReply(string text)
    {
        Debug.Log(text);
    }
}
