using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Improbable.Words;
using Prime31.MessageKit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class ChatPrep : MonoBehaviour {

    [Require] private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;
    
    private bool isTyping = false;

     void Start()
    {
        MessageKit<string>.addObserver(MessageKitIds.SEND_CHAT, SendSpawnEvent);
    }

     void OnDestroy()
    {
        MessageKit<string>.removeObserver(MessageKitIds.SEND_CHAT, SendSpawnEvent);

        isTyping = false;
        MessageKit<bool>.post(MessageKitIds.TYPING_STATE, isTyping);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isTyping = !isTyping;
            MessageKit<bool>.post(MessageKitIds.TYPING_STATE, isTyping);
        }
	}

    public bool IsTyping()
    {
        return isTyping;
    }

    private void SendSpawnEvent(string message)
    {
        SpatialOS.Commands
            .SendCommand(clientAuthorityCheckWriter, MessageSpawner.Commands.Spawn.Descriptor, new MessageSpawnRequest(message), clientAuthorityCheckWriter.EntityId)
            .OnSuccess(OnSpawnSuccess)
            .OnFailure(OnSpawnFailure);
    }

    private void OnSpawnFailure(ICommandErrorDetails response)
    {
        Debug.LogError("Message spawning failed :( " + response.ErrorMessage);
    }

    private void OnSpawnSuccess(MessageSpawnResponse response)
    {
        
    }
}
