using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
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
    
    private void OnEnable()
    {
        transform.position = positionWriter.Data.coords.ToUnityVector();
        
        characterControlsReader.ComponentUpdated.Add(OnCharacterControlsUpdated);

        Debug.LogWarning("Enabled");
    }
    
    private void OnDisable()
    {
        characterControlsReader.ComponentUpdated.Remove(OnCharacterControlsUpdated);
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
}

public static class Vector3Extensions
{
    public static Coordinates ToCoordinates(this Vector3 vector3)
    {
        return new Coordinates(vector3.x, vector3.y, vector3.z);
    }
}