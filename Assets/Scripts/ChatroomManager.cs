﻿using UnityEngine;
using UnityEngine.UI;

public class ChatroomManager : MonoBehaviour
{
    public Message MessagePrefab;

    private Transform messages;
    private float targetTotalOffset;
    private float messageOffset;

    public void Start()
    {
        messages = transform.GetChild(0).GetChild(0);
        targetTotalOffset = ((RectTransform)transform).sizeDelta.y / 2 - 0.4f;
        messages.transform.localPosition = new Vector3(0, targetTotalOffset, 0);
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
        targetTotalOffset = Mathf.Max(((RectTransform)transform).sizeDelta.y / 2 - 0.4f, 
            -((RectTransform)transform).sizeDelta.y / 2 + messageOffset * temp.transform.localScale.y - 0.25f);
    }
}
