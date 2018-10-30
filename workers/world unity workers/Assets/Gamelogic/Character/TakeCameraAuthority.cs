using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class TakeCameraAuthority : MonoBehaviour {
    
    [Require]
    private ClientAuthorityCheck.Writer clientAuthorityCheckWriter;

    private FollowTarget followTarget;

    public void OnEnable()
    {
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
    }
}
