using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Prime31.MessageKit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class NewColliderClientReport : MonoBehaviour {

    [Require] private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;
    [Require] private CollisionsCreated.Reader collisionsCreatedReader;

    private CharacterClientMovement clientMovement; //Source for timestamp

    void Awake()
    {
        clientMovement = GetComponent<CharacterClientMovement>();
    }

    void Start()
    {
        MessageKit<long>.addObserver(MessageKitIds.WALLS_ALIVE, ReportClientSeenNewCollider);
    }

    void OnDestroy()
    {
        MessageKit<long>.removeObserver(MessageKitIds.WALLS_ALIVE, ReportClientSeenNewCollider);
    }

    private void ReportClientSeenNewCollider(long wallEntityId)
    {
        //Only report client seen new collider if server has recently reported. Otherwise this has already been seen :O
        /*bool idFound = false;
        Improbable.Collections.List<NewCollision> newCollisions = collisionsCreatedReader.Data.newCollisions;
        Debug.LogWarning(newCollisions.Count);
        foreach(NewCollision nc in newCollisions)
        {
            if (nc.entityId == wallEntityId)
            {
                idFound = true;
                break;
            }
        }
        if (!idFound)
        {
            Debug.LogWarning("Not reporting client seen wall entity: " + wallEntityId + ", because did not see server report");
            return;
        }*/

        //The above does not work because local changes are not updated fast enough (testing locally)
        //Updating for every message as it is spawned is going to be unecessarily heavy bandwidth on loading a new game / loading a new area
        //of the map, but for this small version of the game I'll allow it

        NewCollision newCollision = new NewCollision();
        newCollision.entityId = wallEntityId;
        newCollision.timestamp = clientMovement.GetLocalTimestamp();

        SpatialOS.Commands
            .SendCommand(clientAuthorityCheckWriter, CollisionsCreated.Commands.ClientCollisionCreated.Descriptor, new ClientCollisionCreatedRequest(newCollision), clientAuthorityCheckWriter.EntityId)
            .OnSuccess(OnClientCollisionCreatedSuccess)
            .OnFailure(OnClientCollisionCreatedFailure);

    }

    private void OnClientCollisionCreatedFailure(ICommandErrorDetails response)
    {
        Debug.LogError("Error sending client seen new collision update: " + response.ErrorMessage);
    }

    private void OnClientCollisionCreatedSuccess(ClientCollisionCreatedResponse response)
    {
        //nada
    }
}
