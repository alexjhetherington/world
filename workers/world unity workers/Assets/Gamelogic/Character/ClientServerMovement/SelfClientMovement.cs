using Improbable.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO DROPPED AUTHORITATIVE TRANSFORM??? Probably fine actually

//TODO How big should timestep be for simulations
public abstract class SelfClientMovement<MI> : MonoBehaviour where MI : MovementInput{
    
    protected float timestamp = 0;
    
    private AuthoritativeTransform authoritativeTransform = null;
    private bool simulateFromLast = false;

    protected List<MI> inputs = new List<MI>();

    protected MovementCalculation<MI> movementCalculation;

    protected abstract MI GetMovementInput();
    protected abstract void SendMovementInputToServer(List<MI> movementInput);
    protected abstract AuthoritativeTransform GetAuthoritativeTransform();
    protected abstract float GetLiveTime();

    protected abstract NewCollision[] GetNewColliders();

    protected void Update()
    {

        MI movementInput = GetMovementInput();
        UpdateMovementInput(movementInput);
        
        AuthoritativeTransform at = GetAuthoritativeTransform(); //TODO turn into an event?
        if ((at != null) && (authoritativeTransform == null || at.timestamp > authoritativeTransform.timestamp))
        {
            simulateFromLast = true;
            authoritativeTransform = at;
            DiscardOldInputs(at.timestamp);
        }
        SimulateUntilNow();

        //Debug.Log("Been alive on server for: " + GetLiveTime() + ". Client reporting lifetime as: " + timestamp + ". Difference of: " + (GetLiveTime() - timestamp));
        /*
         * Movement and position keyframes are all calculated using client time and based on relative time between inputs. This makes
         * it very easy for server "real" movement and client "predicted" movement to be identical.
         * 
         * i.e. server live time is inconsequential to movement calculations (with one exception).
         * 
         * To prevent cheating, the server worker doesn't apply inputs with relative time in the future (although these are still valid inputs!).
         * They will be applied when the present reaches this future.
         * 
         * Server time live time is used to determine whether inputs are in the future or not. In theory server time relative to entity start time
         * and client time relative to entity start time will remain the same and there will be no problems.
         * 
         * When an entity switches workers - there are some frames lost and server time relative to entity start time becomes incorrect; it becomes
         * slightly lower. In this scenario - inputs that are valid in the present won't be applied until the future. This doesn't affect client worker
         * because the prediction is perfect and the relative time hasn't changed - but the canonical state of the world will be incorrect, which is 
         * obviously a problem when you want other players to see the client, or worry about things like hit detection.
         * 
         * The solution used here is to steal some time from the client relative start time. This effectively means one input from the player
         * will be shorter than others and the player character will skip backwards (in "real" terms). The cheeky hack is to do this only when the player
         * is still so they don't notice.
         * 
         * (N.B. if this framework were used with dynamic environments the skip backwards would need to be checked to see if it would introduce
         * any new moving colliders into the equation - although it would require more work to introduce moving colliders. 
         * 
         * Swapping worker takes a few frames? Or takes one frame but something to do with not using fixed udpate causes issues? Does a physics object
         * switching workers have issues as well?)
         */

        //Debug.Log(movementInput.IsNone());
        //Debug.Log(movementCalculation.IsStill());
        if(!(movementInput.IsNone() && movementCalculation.IsStill() && timestamp > GetLiveTime()))
        {
            timestamp += Time.deltaTime;
        }
    }

    public float GetLocalTimestamp()
    {
        return timestamp;
    }

    private void DebugInputs()
    {
        Debug.Log("Inputs Size: " + inputs.Count);
        if(inputs.Count > 0)
        {
            string output = "";
            foreach (MI mi in inputs)
            {
                output = output + mi.timestamp + ", ";
            }
        }
    }
    
    protected void UpdateMovementInput(MI movementInput)
    {
        if (inputs.Count == 0 || movementInput.IsNew(inputs[inputs.Count - 1]))
        {
            //Debug.Log("New Input: " + movementInput.ToString());
            inputs.Add(movementInput);
            SendMovementInputToServer(inputs);
        }
    }

    protected void SetAuthoritativePosition()
    {
        //SET POSITION
        transform.position = authoritativeTransform.position;
        transform.rotation = authoritativeTransform.rotation;
    }

    protected void DiscardOldInputs(float lastAuthoritativeTimestamp)
    {
        int i;
        for (i = inputs.Count - 1; i >= 0; i--)
        {
            if (inputs[i].timestamp < lastAuthoritativeTimestamp)
            {
                //If input is below lastAuthoritativeTimestamp, can remove everything below but not including it
                inputs.RemoveRange(0, i); 
                break;
            }
        }
    }

    protected void SimulateUntilNow()
    {
        if (simulateFromLast)
        {
            /*Debug.Log("---Simulating from new received Authority Transform---");
            Debug.Log("New authority transform: " + authoritativeTransform.ToString());

            Debug.Log("Received at: " + timestamp);

            Dictionary<MI, int> debugSteps = movementCalculation.GetDebugSteps();
            Debug.Log("Steps taken by client since last authority transform.");
            foreach (MI key in debugSteps.Keys)
            {
                Debug.Log("Input: " + key.ToString() + ", steps: " + debugSteps[key]);
            } */

            Vector3 position = gameObject.transform.position;
            SetAuthoritativePosition();
            movementCalculation.DoMovement(inputs, GetNewColliders(), authoritativeTransform.timestamp, timestamp);
            simulateFromLast = false;

            

            Vector3 newPosition = gameObject.transform.position;
            float distance = (newPosition - position).magnitude;

            //Simulating from authority timestamp to this frame should be the same as
            //Simulating from the prior authority timestamp to the frame it was rececieved + movement predicted by client
            if (distance > 0.06)
            {
                Debug.LogError("Incorrect client movement. Distance: " + distance + ", timestamp: " + timestamp);
            }
            //movementCalculation.ResetDebugSteps();
        }


        movementCalculation.DoMovement(inputs, GetNewColliders(), timestamp, timestamp + Time.deltaTime);
    }
}
