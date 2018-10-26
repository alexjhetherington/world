using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Prime31.MessageKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class BecomesVisibleToClient : MonoBehaviour {

    void Start()
    {
        MessageKit<long>.post(MessageKitIds.WALLS_ALIVE, gameObject.GetSpatialOsEntity().EntityId.Id);
    }
}
