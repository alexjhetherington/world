using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Sometimes when a player entity changes worker boundaries, the ServerMovement class fails to re-enable
 * causing all kinds of havoc. My guess is that it's something to do with component authority, so this
 * class also requires the same components. If I see the error happen again and this class fails to enable as 
 * well, I can try reducing the components in this class to narrow down the offending component.
 * 
 * Since adding this class I have yet to see the error tho lol :D (may also have been an update?)
 * I don't want to touch it now
 */ 

[WorkerType(WorkerPlatform.UnityWorker)]
public class DetectiveBoi : MonoBehaviour {

    //[Require] private Position.Writer positionWriter;
    //[Require] private PositionSetTimestamp.Writer timestampWriter;
    //[Require] private CharacterControls.Reader characterControlsReader;
    //[Require] private LiveTime.Writer liveTimeWriter;
    [Require] private CollisionsCreated.Writer collisionsCreatedWriter;

    private void OnEnable()
    {
        Debug.LogWarning("Detective boi activated");
    }

    private void OnDisable()
    {
        Debug.LogWarning("Detective boi deactivated");
    }
}
