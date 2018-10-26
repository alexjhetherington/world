using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class CharacterServerMovement : ServerMovement<CharacterInputs>
{
    [Require] private Position.Writer positionWriter;
    [Require] private PositionSetTimestamp.Writer timestampWriter;
    [Require] private CharacterControls.Reader characterControlsReader;
    [Require] private LiveTime.Writer liveTimeWriter;
    [Require] private CollisionsCreated.Writer collisionsCreatedWriter;

    private void OnEnable()
    {
        transform.position = positionWriter.Data.coords.ToUnityVector();
        
        characterControlsReader.ComponentUpdated.Add(OnCharacterControlsUpdated);
        Debug.LogWarning("Server Movement Enabled");
    }
    
    private void OnDisable()
    {
        characterControlsReader.ComponentUpdated.Remove(OnCharacterControlsUpdated);
        Debug.LogWarning("Server Movement Disabled");
    }

    private void OnCharacterControlsUpdated(CharacterControls.Update obj)
    {
        OnCharacterControlsUpdated(ReceiveMovementInput());
    }

    private void Awake()
    {
        movementCalculation = new MoveCalc(GetComponent<CharacterController>());
    }

    protected override void PublishAuthoritativeTransform(AuthoritativeTransform authoritativeTransform)
    {
        positionWriter.Send(new Position.Update()
            .SetCoords(authoritativeTransform.position.ToCoordinates())
            );

        timestampWriter.Send(new PositionSetTimestamp.Update()
            .SetTimestamp(authoritativeTransform.timestamp)
            );

        RemoveNewCollisions();
    }

    private void RemoveNewCollisions()
    {
        float lastUpdatedTimestamp = timestampWriter.Data.timestamp;

        Improbable.Collections.List<NewCollision> existingNewCollisions = collisionsCreatedWriter.Data.newCollisions.DeepCopy();
        Improbable.Collections.List<NewCollision> newCollisions = new Improbable.Collections.List<NewCollision>(0);
        foreach(NewCollision nc in existingNewCollisions)
        {
            //If last updated timestamp is after the new collision timestamp, we don't need to worry about the new collision anymore
            //we can assume it has always existed
            if (nc.timestamp > lastUpdatedTimestamp)
            {
                newCollisions.Add(nc);
            }
        }

        collisionsCreatedWriter.Send(
            new CollisionsCreated.Update().SetNewCollisions(newCollisions)
            );
    }

    protected override AuthoritativeTransform GetLastPublishedTransform()
    {
        Vector3 position = positionWriter.Data.coords.ToUnityVector();
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        float timestamp = timestampWriter.Data.timestamp;
        AuthoritativeTransform at = new AuthoritativeTransform(timestamp, position, rotation);
        return at;
    }

    protected override List<CharacterInputs> ReceiveMovementInput()
    {
        Improbable.Collections.List<CharacterControlsUpdate> charControls = characterControlsReader.Data.characterControls;


        List<CharacterInputs> output = new List<CharacterInputs>();
        for(int i = 0; i < charControls.Count; i++)
        {
            CharacterControlsUpdate update = charControls[i];
            float horz = update.horizontalAxis;
            float vert = update.verticalAxis;
            bool spirit = update.spiritMode;
            float timestamp = update.timestamp;

            CharacterInputs input = new CharacterInputs(timestamp, horz, vert, spirit);

            output.Add(input);
        }
        
        return output;
    }

    protected override float GetLiveTime()
    {
        return liveTimeWriter.Data.timestamp;
    }

    protected override void PublishLiveTime(float liveTime)
    {
        liveTimeWriter.Send(new LiveTime.Update()
            .SetTimestamp(liveTime)
            );
    }

    protected override NewCollision[] GetNewColliders()
    {
        List<NewCollision> newCollisions = collisionsCreatedWriter.Data.newCollisions;
        return newCollisions.ToArray();
    }
}

public static class Vector3Extensions
{
    public static Coordinates ToCoordinates(this Vector3 vector3)
    {
        return new Coordinates(vector3.x, vector3.y, vector3.z);
    }
}