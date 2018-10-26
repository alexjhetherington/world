using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMessages {

    private Dictionary<long, GameObject> messages = new Dictionary<long, GameObject>();

    private static AllMessages instance;
    public static AllMessages GetInstance()
    {
        if(instance == null)
        {
            instance = new AllMessages();
        }

        return instance;
    }

    public void AddMessage(long id, GameObject message)
    {
        messages.Add(id, message);
    }

    public void RemoveMessage(long id)
    {
        if (messages.ContainsKey(id))
        {
            messages.Remove(id);
        }
        else
        {
            Debug.LogError("Error removing message. Key: " + id);
        }
    }

    public GameObject GetMessage(long id)
    {
        if (messages.ContainsKey(id))
        {
            return messages[id];
        }
        else
        {
            Debug.LogError("Error getting message. Key: " + id);
            return null;
        }
    }
}
