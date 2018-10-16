using Improbable;
using Improbable.Entity.Component;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Improbable.Words;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class MessageLayer : MonoBehaviour {

    [Require] private MessageSpawner.Writer messageSpawnerWriter;
    //[Require] private Position.Writer positionWriter;

    private void OnEnable()
    {
        messageSpawnerWriter.CommandReceiver.OnSpawn.RegisterAsyncResponse(CreateMessageOnGround);
    }

    private void CreateMessageOnGround(ResponseHandle<MessageSpawner.Commands.Spawn, MessageSpawnRequest, MessageSpawnResponse> handle)
    {
        Debug.LogWarning("Received Command");
        var initialPosition = transform.position;
        var messageOnGroundTemplate = EntityTemplateFactory.CreateMessageOnGroundTemplate(initialPosition, handle.Request.message, true);
        SpatialOS.Commands.CreateEntity(messageSpawnerWriter, messageOnGroundTemplate);
        //Find all nearby characters and add the default collision time (now + 3 seconds
        //The client will report their collision time when they get the update, which may update this collision time)
        handle.Respond(new MessageSpawnResponse());
    }

    private void OnDisable()
    {
        messageSpawnerWriter.CommandReceiver.OnSpawn.DeregisterResponse();
    }
}
