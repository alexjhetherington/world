using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class CharacterClientMovement : SelfClientMovement<CharacterInputs>
{
    [Require] private CharacterControls.Writer characterControlsWriter;
    [Require] private Position.Reader positionReader;
    [Require] private PositionSetTimestamp.Reader timestampReader;
    [Require] private LiveTime.Reader liveTimeReader;

    [Require] private CollisionsCreated.Reader collisionsCreatedReader;

    private ChatPrep chatPrep;

    private void Awake()
    {
        movementCalculation = new MoveCalc(GetComponent<CharacterController>());
        chatPrep = GetComponent<ChatPrep>();
    }

    protected override CharacterInputs GetMovementInput()
    {
        if (!chatPrep.IsTyping())
        {
            return new CharacterInputs(timestamp, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetKey(KeyCode.LeftShift));
        }
        else
        {
            return new CharacterInputs(timestamp, 0, 0, false);
        }
    }

    protected override void SendMovementInputToServer(List<CharacterInputs> movementInput)
    {
        Improbable.Collections.List<CharacterControlsUpdate> characterControls = new Improbable.Collections.List<CharacterControlsUpdate>();
        for (int i = 0; i < movementInput.Count; i++)
        {
            CharacterInputs input = movementInput[i];

            CharacterControlsUpdate update = new CharacterControlsUpdate();
            update.horizontalAxis = input.horizontalAxis;
            update.verticalAxis = input.verticalAxis;
            update.spiritMode = input.spiritMode;
            update.timestamp = input.timestamp;

            characterControls.Add(update);
        }
        characterControlsWriter.Send(new CharacterControls.Update().SetCharacterControls(characterControls));
    }

    protected override AuthoritativeTransform GetAuthoritativeTransform()
    {
        Vector3 position = positionReader.Data.coords.ToUnityVector();
        Quaternion rotation = Quaternion.Euler(0, 0,0);
        float timestamp = timestampReader.Data.timestamp;
        AuthoritativeTransform at = new AuthoritativeTransform(timestamp, position, rotation);
        return at;
    }

    protected override float GetLiveTime()
    {
        return liveTimeReader.Data.timestamp;
    }

    protected override NewCollision[] GetNewColliders()
    {
        List<NewCollision> newCollisions = collisionsCreatedReader.Data.newCollisions;
        return newCollisions.ToArray();
    }
}