using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class OtherCharacterClientMovement : OtherClientMovement<CharacterInputs>
{
    [Require] private CharacterControls.Reader characterControlsReader;
    [Require] private Position.Reader positionReader;
    [Require] private PositionSetTimestamp.Reader timestampReader;

    [Require] private CollisionsCreated.Reader collisionsCreatedReader;

    private void Awake()
    {
        movementCalculation = new MoveCalc(GetComponent<CharacterController>());
    }
    
    private void OnEnable()
    {
        characterControlsReader.ComponentUpdated.Add(OnCharacterControlsUpdated);
        
        if (characterControlsReader.Authority == Improbable.Worker.Authority.Authoritative)
        {
            //This character belongs to this client, so disable this script
            List<MonoBehaviour> wrapper = new List<MonoBehaviour>();
            wrapper.Add(this);
            gameObject.GetSpatialOsEntity().Visualizers.DisableVisualizers(wrapper);
        }
        else
        {
            //Other clients are rendered slightly in the past. We keep old inputs from them to enable this.
            //When another client is enabled, old inputs might have already been discarded. In this scenario
            //We assume the old inputs are no movement
            List<CharacterInputs> fakeInputs = new List<CharacterInputs>();
            fakeInputs.Add(new CharacterInputs(0, 0, 0, false));
            UpdateMovementInputs(fakeInputs);

            UpdateMovementInputs(ReceiveMovementInput());
            AuthoritativeTransform at = GetAuthoritativeTransform();
            SetLast(at);
            prep = at;
        }
    }

    private void OnDisable()
    {
        characterControlsReader.ComponentUpdated.Remove(OnCharacterControlsUpdated);
    }

    private void OnCharacterControlsUpdated(CharacterControls.Update obj)
    {
        OnCharacterControlsUpdated(ReceiveMovementInput());
    }

    //TODO Shared method?
    protected List<CharacterInputs> ReceiveMovementInput()
    {
        Improbable.Collections.List<CharacterControlsUpdate> charControls = characterControlsReader.Data.characterControls;


        List<CharacterInputs> output = new List<CharacterInputs>();
        for (int i = 0; i < charControls.Count; i++)
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

    //TODO Shared method?
    protected override AuthoritativeTransform GetAuthoritativeTransform()
    {
        Vector3 position = positionReader.Data.coords.ToUnityVector();
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        float timestamp = timestampReader.Data.timestamp;
        AuthoritativeTransform at = new AuthoritativeTransform(timestamp, position, rotation);
        return at;
    }

    protected override NewCollision[] GetNewColliders()
    {
        List<NewCollision> newCollisions = collisionsCreatedReader.Data.newCollisions;
        return newCollisions.ToArray();
    }
}
