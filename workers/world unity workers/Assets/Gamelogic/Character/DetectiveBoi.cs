using Improbable;
using Improbable.Character;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class DetectiveBoi : MonoBehaviour {

    [Require] private Position.Writer positionWriter;
    [Require] private PositionSetTimestamp.Writer timestampWriter;
    [Require] private CharacterControls.Reader characterControlsReader;
    [Require] private LiveTime.Writer liveTimeWriter;
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
