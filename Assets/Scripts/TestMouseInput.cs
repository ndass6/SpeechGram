using UnityEngine;

public class TestMouseInput : MonoBehaviour
{
    public ChatroomManager Chatroom;
    public HoverMenu HoverMenu;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HoverMenu.ToggleMenu();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            string text = "Test message: ";
            for(int i = 0; i < Random.Range(0, 20); i++)
                text += "a ";
            Chatroom.AddMessage("Guy", text);
        }
    }
}