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

            Chatroom.Disappear();
        }
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        StartCoroutine(MenuAppear(0));
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
        yield return new WaitForSeconds(0.1f);
        RegisterButton.Disappear();
        yield return new WaitForSeconds(0.1f);
        CloseButton.Disappear();

        int[] list = new int[] { 3, 7, 11, 10, 9, 8, 4, 0, 1, 2, 6, 5 };
        for(int i = 0; i < MenuButton.NUM_BUTTONS; i++)
        {
            yield return new WaitForSeconds(0.04f);
            ReplyButtons[list[i]].Disappear();
        }

        yield return new WaitForSeconds(0.6f);

        gameObject.SetActive(false);
        Chatroom.Appear();
    }

    private IEnumerator MenuReplace(int menu)
    {
        if(menu == 0)
            BackButton.Deactivate();
        else
            BackButton.Reactivate();

        ArrayList list = new ArrayList();
        for(int i = 0; i < MenuButton.NUM_BUTTONS; i++)
            list.Add(i);

        while(list.Count != 0)
        {
            int index = Random.Range(0, list.Count);
            int current = (int)list[index];
            list.RemoveAt(index);
            ReplyButtons[current].Replace(ButtonData.Buttons[menu, current]);

            yield return new WaitForSeconds(0.05f);
        }
    }

    #endregion
}
