using Improbable;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Words;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageTransformSync : MonoBehaviour {

    [Require] private Position.Reader positionReader;
    [Require] private Message.Reader messageReader;

    private void OnEnable()
    {
        transform.position = positionReader.Data.coords.ToUnityVector();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        TMP_Text tmpText = GetComponentInChildren<TMP_Text>();
        tmpText.text = messageReader.Data.message;
        tmpText.ForceMeshUpdate();

        MessageStack stacker = GetComponentInChildren<MessageStack>();
        if (stacker != null)
        {
            stacker.Stack();
        }

        Walls walls = GetComponentInChildren<Walls>();
        walls.SizeWalls();

        AllMessages.GetInstance().AddMessage(gameObject.GetSpatialOsEntity().EntityId.Id, walls.gameObject);
    }

    private void OnDisable()
    {
        AllMessages.GetInstance().RemoveMessage(gameObject.GetSpatialOsEntity().EntityId.Id);
    }
}
