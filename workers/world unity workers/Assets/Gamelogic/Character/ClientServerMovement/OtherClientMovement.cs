using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OtherClientMovement<MI> : MonoBehaviour where MI : MovementInput
{
    //Delays character from displaying by this length of time.
    //Higher values will prevent weird behaviour around keyframes when latency is unstable
    protected float delay = 0.4f;

    private List<MI> movementInputsIncludingOld = new List<MI>();

    protected MovementCalculation<MI> movementCalculation;
    
    private AuthoritativeTransform last;
    protected AuthoritativeTransform prep;

    private float relativeTime;

    private float smoothTimeScale;

    protected abstract AuthoritativeTransform GetAuthoritativeTransform();

    private void Update()
    {
        //TODO kind of copied code
        AuthoritativeTransform at = GetAuthoritativeTransform(); //TODO turn into an event?
        if ((at != null) && (prep == null || at.timestamp > prep.timestamp))
        {
            prep = at;
            //Debug.Log("Prepping: " + at.ToString());
            

            //If there is no variance in updating time, this will be equal to delay
            //If it's smaller, we need to increase the pace of the simulation
            float timeDifference = prep.timestamp - (last.timestamp + relativeTime);
            

            if (timeDifference <= 0)
            {
                //Debug.Log("Relative time already past new transform, setting new transform. Will jump backwards in time");
                SetLast(at);
                smoothTimeScale = 1;
            }
            else
            {
                StartCoroutine(SetLastAfterDelay(at, delay));
                //Debug.Log("Time Difference: " + timeDifference + ", delay: " + delay);
                smoothTimeScale = delay / timeDifference;
            }
        }

        if (last != null)
        {
            float startTime = last.timestamp + relativeTime;

            float stepAmount;
            stepAmount = Time.deltaTime * smoothTimeScale;
            
            relativeTime = relativeTime + stepAmount;
            float endTime = last.timestamp + relativeTime;

            //Debug.Log("Start time: " + startTime + ", End Time: " + endTime + ", relativeTime: " + relativeTime);

            movementCalculation.DoMovement(movementInputsIncludingOld, startTime, endTime);
        }
    }
    
    protected void OnAuthoritativeTransformUpdated(AuthoritativeTransform authoritativeTransform)
    {
        StartCoroutine(SetLastAfterDelay(authoritativeTransform, delay));
    }

    protected IEnumerator SetLastAfterDelay(AuthoritativeTransform authoritativeTransform, float delayTime)
    {
        //Debug.Log("Prepping new AT after delay 2 : " + authoritativeTransform.ToString());
        yield return new WaitForSeconds(delayTime);
        SetLast(authoritativeTransform);
    }

    protected void SetLast(AuthoritativeTransform authoritativeTransform)
    {
        last = authoritativeTransform;
        DiscardOldInputs(last.timestamp);

        Vector3 before = transform.position;
        transform.position = last.position;
        Vector3 after = transform.position;
        float magnitude = (after - before).magnitude;
        if(magnitude > 0.6)
        {
            Debug.Log("Jumped!: " + (after - before).magnitude);
        }
        
        smoothTimeScale = 1;
        relativeTime = 0;
    }

    protected void OnCharacterControlsUpdated(List<MI> movementInput)
    {
        UpdateMovementInputs(movementInput);
    }

    protected void UpdateMovementInputs(List<MI> movementInput)
    {
        float latestTimestamp;
        if (movementInputsIncludingOld.Count > 0)
        {
            latestTimestamp = movementInputsIncludingOld[movementInputsIncludingOld.Count - 1].timestamp;
        }
        else
        {
            latestTimestamp = -1;
        }

        int i;
        for (i = 0; i < movementInput.Count; i++)
        {
            if (movementInput[i].timestamp > latestTimestamp)
            {
                List<MI> newInputs = movementInput.GetRange(i, movementInput.Count - i);
                if (last != null)
                {
                    float timestamp = newInputs[0].timestamp;
                    float clientPresentTime = (last.timestamp + relativeTime) - delay;
                    if (timestamp < clientPresentTime)
                    {
                        Debug.Log("Inputs arriving late. Will skip forward somtimes.");
                    }
                }
                //Debug.Log("Adding new inputs - earliest: " + newInputs[0] + ", latest: " + newInputs[newInputs.Count-1]);
                movementInputsIncludingOld.AddRange(newInputs);
                return;
            }
        }
    }

    //TODO shared code
    protected void DiscardOldInputs(float timestamp)
    {
        int i;
        for (i = movementInputsIncludingOld.Count - 1; i >= 0; i--)
        {
            if (movementInputsIncludingOld[i].timestamp < timestamp)
            {
                //Debug.Log("Removing from before: " + timestamp);
                movementInputsIncludingOld.RemoveRange(0, i);
                break;
            }
        }
    }
}
