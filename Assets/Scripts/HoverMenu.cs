using HoloToolkit.Unity;
using System.Collections;
using UnityEngine;

public class HoverMenu : MonoBehaviour
{
    public MicrophoneManager Microphone;
    public TextToSpeechManager TextSpeech;
    public ChatroomManager Chatroom;

    public MenuButton BackButton;
    public MenuButton RegisterButton;
    public MenuButton CloseButton;
    public MenuButton[] ReplyButtons = new MenuButton[MenuButton.NUM_BUTTONS];

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
                if(button != null && button.Active)
                {
                    bool close = true;
                    switch(button.Data.Type)
                    {
                        case ButtonType.Back:
                            StartCoroutine(MenuReplace(0));
                            close = false;
                            break;
                        case ButtonType.Register:
                            Microphone.registeringNewUser = true;
                            break;
                        case ButtonType.Navigation:
                            StartCoroutine(MenuReplace(button.Data.Destination));
                            close = false;
                            break;
                        case ButtonType.Reply:
                            Chatroom.AddMessage("Me", button.Data.Message);
                            TextSpeech.SpeakText(button.Data.Message);
                            break;
                    }

                    if(close)
                    {
                        StartCoroutine(MenuDisappear());
                    }
                }
            }
        }
        else
        {
            // Set menu position to directly in front of user
            transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;

            gameObject.SetActive(true);
            Chatroom.gameObject.SetActive(false);

            StartCoroutine(MenuAppear(0));
        }
    }

    #region Animation coroutines

    private IEnumerator MenuAppear(int menu)
    {
        BackButton.Appear(new ButtonData(ButtonType.Back, "Go Back"));
        BackButton.Deactivate();
        RegisterButton.Appear(new ButtonData(ButtonType.Register, "Register New Speaker"));
        CloseButton.Appear(new ButtonData(ButtonType.Close, "Close Menu"));
        for(int i = 0; i < MenuButton.NUM_BUTTONS; i++)
            ReplyButtons[i].Appear(ButtonData.Buttons[menu, i]);
        yield return null;
    }

    private IEnumerator MenuDisappear()
    {
        BackButton.Disappear();
        RegisterButton.Disappear();
        CloseButton.Disappear();
        for(int i = 0; i < MenuButton.NUM_BUTTONS; i++)
            ReplyButtons[i].Disappear();

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
        Chatroom.gameObject.SetActive(true);
    }

    private IEnumerator MenuReplace(int menu)
    {
        if(menu == 0)
            BackButton.Deactivate();
        else
            BackButton.Reactivate();

        for(int i = 0; i < MenuButton.NUM_BUTTONS; i++)
            ReplyButtons[i].Replace(ButtonData.Buttons[menu, i]);
        yield return null;
    }

    #endregion
}
