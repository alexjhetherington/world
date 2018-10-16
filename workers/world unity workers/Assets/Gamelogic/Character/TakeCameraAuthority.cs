using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO deal with interpolation and untether
//Script will live on camera and this script will simply set its target

[WorkerType(WorkerPlatform.UnityClient)]
public class TakeCameraAuthority : MonoBehaviour {

    //We use this empty component that only client id has write access too
    //My understanding is that we could use the input component for this same purpose but it seems a bit messy
    [Require]
    private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;

    private Transform Camera;

    public void OnEnable()
    {
        Camera = FindObjectOfType<Camera>().transform;
        Camera.SetParent(gameObject.transform);
        Camera.localPosition = new Vector3(0, 999, 0);
    }

    public void OnDisable()
    {
        Camera.SetParent(null);
    }
}
