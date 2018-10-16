using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO QUEUE OF INPUTS, NOT JUST ONE. Sometimes when a user spams inputs they are overriden
public abstract class ServerMovement<MI> : MonoBehaviour where MI : MovementInput
{    
    protected MovementCalculation<MI> movementCalculation;

    //private float delayTime = 1f;
    //private AuthorityQueue authorityQueue = new AuthorityQueue();

    protected abstract List<MI> ReceiveMovementInput();
    protected abstract void PublishAuthoritativeTransform(AuthoritativeTransform authoritativeTransform);
    protected abstract AuthoritativeTransform GetLastPublishedTransform();
    protected abstract float GetLiveTime();
    protected abstract void PublishLiveTime(float liveTime);
    

    // Update is called once per frame
    // The order of stuff happening here is very important and finnicky
    void Update () {
        float liveTime = GetLiveTime();
        PublishLiveTime(liveTime + Time.deltaTime);
    }

    //Here it's guaranteed to be new :O
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
        movementCalculation.DoMovement(movementInput, lastPublishedTransform.timestamp, simToTime);

        AuthoritativeTransform toPublish = GetAuthoritativeTransform(simToTime);
        PublishAuthoritativeTransform(toPublish);
        //Debug.LogWarning("New authority transform: " + toPublish);
    }

    /*protected AuthoritativeTransform QueueAuthorityTransform(AuthoritativeTransform newAuthorityTransform)
    {
        authorityQueue.Tick(Time.deltaTime);
        authorityQueue.Insert(newAuthorityTransform);
        if (authorityQueue.PeekAliveFor() >= delayTime)
        {
            return authorityQueue.Pop();
        }
        else
        {
            return null;
        }
        
    }*/
    
    protected AuthoritativeTransform GetAuthoritativeTransform(float timestamp)
    {
        return new AuthoritativeTransform(timestamp, transform.position, transform.rotation);
    }
}
