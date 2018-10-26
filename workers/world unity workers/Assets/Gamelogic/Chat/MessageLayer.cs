﻿using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Entity.Component;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Entity;
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
        messageSpawnerWriter.CommandReceiver.OnSpawn.RegisterAsyncResponse(ReserveEntityIdForCreateMessage);
    }

    private void ReserveEntityIdForCreateMessage(ResponseHandle<MessageSpawner.Commands.Spawn, MessageSpawnRequest, MessageSpawnResponse> handle)
    {        
        SpatialOS.Commands.ReserveEntityId(messageSpawnerWriter)
            .OnSuccess(result => { CreateMessageOnGround(result.ReservedEntityId, handle); })
            .OnFailure(OnFailedReservation);

        handle.Respond(new MessageSpawnResponse());
    }

    /*public void Wait(EntityId reserve, ResponseHandle<MessageSpawner.Commands.Spawn, MessageSpawnRequest, MessageSpawnResponse> handle)
    {
        StartCoroutine(CreateMessageOnGround(reserve, handle));
    }*/

    public void CreateMessageOnGround(EntityId reserve, ResponseHandle<MessageSpawner.Commands.Spawn, MessageSpawnRequest, MessageSpawnResponse> handle)
    {
        var initialPosition = transform.position;
        var messageOnGroundTemplate = EntityTemplateFactory.CreateMessageOnGroundTemplate(initialPosition, handle.Request.message, true);

        //Find all nearby characters and add the default collision time (now + 3 seconds
        //The client will report their collision time when they get the update, which may update this collision time)
        try
        {
            GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
            foreach (GameObject character in characters)
            {
                IEntityObject entity = character.GetSpatialOsEntity();

                NewCollision newCollision = new NewCollision();
                float currentTimestamp = SpatialOS.GetLocalEntityComponent<LiveTime>(entity.EntityId).Get().Value.timestamp;
                newCollision.timestamp = currentTimestamp + 3; //TODO extract
                newCollision.entityId = reserve.Id;

                SpatialOS.Commands
                    .SendCommand(messageSpawnerWriter, CollisionsCreated.Commands.ServerCollisionCreated.Descriptor, new ServerCollisionCreatedRequest(newCollision), entity.EntityId)
                    .OnSuccess(OnServerCollisionCreatedSuccess)
                    .OnFailure(OnServerCollisionCreatedFailure);
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Failed to update all characters with new collision. " + e.StackTrace);
        }

        SpatialOS.Commands.CreateEntity(messageSpawnerWriter, reserve, messageOnGroundTemplate)
        .OnFailure(failure => Debug.LogError("Failed to create message :O" + failure.ErrorMessage)); //TODO
    }

    private void OnFailedReservation(ICommandErrorDetails response)
    {
        Debug.LogError("Failed to reserve entity id. Won't make message! " + response.ErrorMessage);
    }

    private void OnServerCollisionCreatedFailure(ICommandErrorDetails response)
    {
        Debug.LogError("Failed to set server new collision. Entities will interact with collider as if it has always existed. " + response.ErrorMessage);
    }

    private void OnServerCollisionCreatedSuccess(ServerCollisionCreatedResponse response)
    {
        //Nada
    }

    private void OnDisable()
    {
        messageSpawnerWriter.CommandReceiver.OnSpawn.DeregisterResponse();
    }
}
