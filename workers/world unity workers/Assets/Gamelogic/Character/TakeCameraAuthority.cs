﻿using Improbable.Core;
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

    private FollowTarget followTarget;

    public void OnEnable()
    {
        //Camera = FindObjectOfType<Camera>().transform;
        //Camera.SetParent(gameObject.transform);
        //Camera.localPosition = new Vector3(0, 999, 0);

        StartCoroutine(SetupTarget());
    }

    IEnumerator SetupTarget()
    {
        //Wait a frame so checked out entity can be set to the correct place
        yield return null;

        Camera cam = FindObjectOfType<Camera>();
        followTarget = cam.gameObject.GetComponent<FollowTarget>();
        followTarget.SetTarget(gameObject);
    }

    public void OnDisable()
    {
        followTarget.SetTarget(null);
        //Camera.SetParent(null);
    }
}
