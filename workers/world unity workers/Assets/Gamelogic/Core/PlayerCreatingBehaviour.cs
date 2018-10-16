using Improbable;
using Improbable.Core;
using Improbable.Entity.Component;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO what causes errors?????
[WorkerType(WorkerPlatform.UnityWorker)]
public class PlayerCreatingBehaviour : MonoBehaviour {

    [Require] private PlayerCreation.Writer playerCreationWriter;

    void OnEnable()
    {
        playerCreationWriter.CommandReceiver.OnCreatePlayer.RegisterResponse(OnCreatePlayer);
    }

    void OnDisable()
    {
        playerCreationWriter.CommandReceiver.OnCreatePlayer.DeregisterResponse();
    }

    private CreatePlayerResponse OnCreatePlayer(CreatePlayerRequest request, ICommandCallerInfo callerInfo)
    {
        CreatePlayerWithReservedId(callerInfo.CallerWorkerId);
        return new CreatePlayerResponse();
    }

    private void CreatePlayerWithReservedId(string clientWorkerId)
    {
        SpatialOS.Commands.ReserveEntityId(playerCreationWriter)
                .OnSuccess(result => CreatePlayer(clientWorkerId, result.ReservedEntityId))
                .OnFailure(failure => OnFailedReservation(failure, clientWorkerId));
    }

    private void OnFailedReservation(ICommandErrorDetails failure, string clientWorkerId)
    {
        Debug.LogError("Failed to Reserve EntityId for Player: " + failure.ErrorMessage + ". Retrying...");
        CreatePlayerWithReservedId(clientWorkerId);
    }

    private void CreatePlayer(string clientWorkerId, EntityId reservedEntityId)
    {
        var initialPosition = new Vector3(0, 0, 0);
        var playerEntityTemplate = EntityTemplateFactory.CreatePlayerCharacterTemplate(clientWorkerId, initialPosition);
        SpatialOS.Commands.CreateEntity(playerCreationWriter, reservedEntityId, playerEntityTemplate)
            .OnFailure(failure => OnFailedPlayerCreation(failure, clientWorkerId, reservedEntityId));
    }

    private void OnFailedPlayerCreation(ICommandErrorDetails failure, string clientWorkerId, EntityId reservedEntityId)
    {
        Debug.LogError("Failed to Create PlayerShip Entity: " + failure.ErrorMessage + ". Retrying...");
        CreatePlayer(clientWorkerId, reservedEntityId);
    }
}
