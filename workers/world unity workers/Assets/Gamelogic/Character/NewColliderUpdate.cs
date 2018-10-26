using Improbable.Character;
using Improbable.Entity.Component;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class NewColliderUpdate : MonoBehaviour {

    [Require] private CollisionsCreated.Writer collisionsCreatedWriter;

    private void OnEnable()
    {
        collisionsCreatedWriter.CommandReceiver.OnServerCollisionCreated.RegisterResponse(OnServerCollisionCreated);
        collisionsCreatedWriter.CommandReceiver.OnClientCollisionCreated.RegisterResponse(OnClientCollisionCreated);
    }

    private void OnDisable()
    {
        collisionsCreatedWriter.CommandReceiver.OnServerCollisionCreated.DeregisterResponse();
        collisionsCreatedWriter.CommandReceiver.OnClientCollisionCreated.DeregisterResponse();
    }

    private Improbable.Collections.List<NewCollision> GetCollisions()
    {
        return collisionsCreatedWriter.Data.newCollisions.DeepCopy();
    }

    private ClientCollisionCreatedResponse OnClientCollisionCreated(ClientCollisionCreatedRequest request, ICommandCallerInfo callerInfo)
    {
        Debug.LogWarning("Received CLIENT collision update with id: " + request.newCollision.entityId + ", timestamp: " + request.newCollision.timestamp);
        Improbable.Collections.List<NewCollision> newCollisions = GetCollisions();
        for(int i = 0; i < newCollisions.Count; i++)
        {
            
            NewCollision nc = newCollisions[i];
            if (nc.entityId == request.newCollision.entityId)
            {
                Debug.LogWarning(nc.timestamp);
                Debug.LogWarning(request.newCollision.timestamp);
                if (nc.timestamp > request.newCollision.timestamp)
                {
                    Debug.LogWarning("made it!");

                    NewCollision replaceNc = new NewCollision();
                    replaceNc.timestamp = request.newCollision.timestamp;
                    replaceNc.entityId = request.newCollision.entityId;
                    newCollisions[i] = replaceNc;

                    break;
                }
            }
        }

        collisionsCreatedWriter.Send(
            new CollisionsCreated.Update().SetNewCollisions(newCollisions)
            );

        return new ClientCollisionCreatedResponse();
    }

    private ServerCollisionCreatedResponse OnServerCollisionCreated(ServerCollisionCreatedRequest request, ICommandCallerInfo callerInfo)
    {
        Debug.LogWarning("Received SERVER collision update with id: " + request.newCollision.entityId + ", timestamp: " + request.newCollision.timestamp);
        Improbable.Collections.List<NewCollision> newCollisions = GetCollisions();
        newCollisions.Add(request.newCollision);

        collisionsCreatedWriter.Send(
            new CollisionsCreated.Update().SetNewCollisions(newCollisions)
            );
        return new ServerCollisionCreatedResponse();
    }
}
