using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomManager : MonoBehaviour
{
    public Message MessagePrefab;

    private Transform messages;

    public void Start()
    {
        messages = transform.FindChild("Messages");
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
            AddMessage("Guy", "Test message");
    }

    public void AddMessage(string speaker, string message)
    {
        Message temp = Instantiate(MessagePrefab, transform.position, Quaternion.identity);
        temp.SetMessage(speaker, message);

        // TODO Offset tracking
        temp.transform.SetParent(messages);
    }
}
