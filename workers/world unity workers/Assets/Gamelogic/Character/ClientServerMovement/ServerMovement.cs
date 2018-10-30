using Improbable.Character;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ServerMovement<MI> : MonoBehaviour where MI : MovementInput
{    
    protected MovementCalculation<MI> movementCalculation;
    
    protected abstract List<MI> ReceiveMovementInput();
    protected abstract void PublishAuthoritativeTransform(AuthoritativeTransform authoritativeTransform);
    protected abstract AuthoritativeTransform GetLastPublishedTransform();
    protected abstract float GetLiveTime();
    protected abstract void PublishLiveTime(float liveTime);

    protected abstract NewCollision[] GetNewColliders();
    
    void Update () {
        float liveTime = GetLiveTime();
        PublishLiveTime(liveTime + Time.deltaTime);
    }
    
    protected void OnCharacterControlsUpdated(List<MI> movementInput)
    {
        /*Debug.LogWarning("---Calculating new authority transform---");
        foreach(MI mi in movementInput)
        {
            Debug.LogWarning(mi.ToString());
        }*/
        
        AuthoritativeTransform lastPublishedTransform = GetLastPublishedTransform();
        transform.position = lastPublishedTransform.position;

        //Debug.LogWarning("Last transform: " + lastPublishedTransform.ToString());

        MI mostRecent = movementInput[movementInput.Count - 1];
        float simToTime = Mathf.Min(mostRecent.timestamp, GetLiveTime());
        movementCalculation.DoMovement(movementInput, GetNewColliders(), lastPublishedTransform.timestamp, simToTime);

        AuthoritativeTransform toPublish = GetAuthoritativeTransform(simToTime);
        PublishAuthoritativeTransform(toPublish);
        //Debug.LogWarning("New authority transform: " + toPublish);
    }
    
    protected AuthoritativeTransform GetAuthoritativeTransform(float timestamp)
    {
        return new AuthoritativeTransform(timestamp, transform.position, transform.rotation);
    }
}
