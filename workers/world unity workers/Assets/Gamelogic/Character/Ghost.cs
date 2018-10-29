using Improbable;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class Ghost : MonoBehaviour {

    [Require] private Position.Reader positionReader;

    public SpriteRenderer ghostSprite;

    void Awake()
    {
        ghostSprite.enabled = false;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ghostSprite.enabled = !ghostSprite.enabled;
        }

        if (ghostSprite.enabled)
        {
            ghostSprite.gameObject.transform.position = positionReader.Data.coords.ToUnityVector();
        }
        
    }
}
