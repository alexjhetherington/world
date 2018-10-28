using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class MessageStack : MonoBehaviour {

    public void Stack()
    {
        GameObject messageParent = GameObject.FindGameObjectWithTag("MessageStack");
        transform.SetParent(messageParent.transform);
        transform.SetAsFirstSibling();

        if (messageParent.transform.childCount == 1)
        {
            transform.position = new Vector3(transform.position.x, -1000, transform.position.z);
        }
        else
        {
            Transform nextSibling = messageParent.transform.GetChild(1);
            transform.position = new Vector3(transform.position.x, nextSibling.transform.position.y + 0.1f, transform.position.z);
        }
    }
}
