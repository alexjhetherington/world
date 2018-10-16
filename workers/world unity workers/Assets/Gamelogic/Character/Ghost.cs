using Improbable;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class Ghost : MonoBehaviour {

    [Require] private Position.Reader positionReader;

    public SpriteRenderer directionSprite;

    private void LateUpdate()
    {
        directionSprite.gameObject.transform.position = positionReader.Data.coords.ToUnityVector();
    }
}
