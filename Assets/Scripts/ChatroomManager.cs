using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatroomManager : MonoBehaviour
{
    public Message MessagePrefab;

    private Transform messages;
    private float targetTotalOffset;
    private float messageOffset;

    public void Start()
    {
        messages = transform.FindChild("Messages");
        targetTotalOffset = ((RectTransform)transform).sizeDelta.y / 2 - 0.15f;
        messages.transform.localPosition = new Vector3(0, targetTotalOffset, 0);
    }

    public void Update()
    {
        // Temporary input
        if(Input.GetMouseButtonDown(0))
        {
            string text = "Test message: ";
            for(int i = 0; i < Random.Range(0, 20); i++)
                text += "a ";
            AddMessage("Guy", text);
        }
    }

    public void FixedUpdate()
    {
        messages.transform.localPosition = Vector3.Lerp(messages.transform.localPosition, new Vector3(0, targetTotalOffset, 0), 0.1f);
    }

    public void AddMessage(string speaker, string message)
    {
        // Create message
        Message temp = Instantiate(MessagePrefab, messages.transform.position, transform.localRotation);
        temp.SetMessage(speaker, message);

        // Calculate offsets
        temp.transform.SetParent(messages);
        float height = temp.GetComponent<Text>().preferredHeight;
        temp.transform.localPosition += new Vector3(0, -messageOffset * temp.transform.localScale.y, 0);
        messageOffset += height;
        targetTotalOffset = Mathf.Max(((RectTransform)transform).sizeDelta.y / 2 - 0.15f, 
            -((RectTransform)transform).sizeDelta.y / 2 + messageOffset * temp.transform.localScale.y - 0.1f);
    }
}
