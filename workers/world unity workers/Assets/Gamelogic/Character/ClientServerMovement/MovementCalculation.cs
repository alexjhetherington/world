
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementCalculation<MI> where MI : MovementInput
{
    protected abstract void DoMovement(MI movementInput, float deltaTime);
    public abstract bool IsStill();

    //TODO put this stuff in the abstract class
    private float stepSize = 1f / 50f;

    public void DoMovement(List<MI> movementInputs, float simulateFromTime, float simulateToTime, bool debug = false)
    {
        if(simulateFromTime > simulateToTime)
        {
            return;
        }

        if((movementInputs.Count > 0 && movementInputs[0].timestamp > simulateFromTime) ||
            (movementInputs.Count == 0 && simulateToTime - simulateFromTime > 0))
        {
            string error = "Unable to simulate total time! Start: " + simulateFromTime + ", End: " + simulateToTime + ", Movement Inputs: ";
            foreach (MI mi in movementInputs)
            {
                error += mi + ", ";
            }
            Debug.LogError(error);
        }

        for(int i = 0; i < movementInputs.Count; i++)
        {
            MI mi = movementInputs[i];
            float startChunk = mi.timestamp;
            float endChunk;
            if (i < movementInputs.Count - 1)
            {
                endChunk = movementInputs[i + 1].timestamp;
            }
            else
            {
                endChunk = float.PositiveInfinity;
            }

            float simulateToForThisInput;
            float simulateFromForThisInput;

            if(endChunk < simulateFromTime || startChunk > simulateToTime)
            {
                continue;
            }

            simulateFromForThisInput = Mathf.Max(simulateFromTime, startChunk);
            simulateToForThisInput = Mathf.Min(simulateToTime, endChunk);
            
            int startStep = (int) (simulateFromForThisInput / stepSize);
            int endStep = (int)(simulateToForThisInput / stepSize);

            int steps = endStep - startStep;

            if (debug)
            {
                Debug.LogWarning("Doing movement for input: " + mi.ToString() + ", from: " + simulateFromForThisInput + ", to: " + simulateToForThisInput + ". STEPS: " + steps);
            }
            AddDebugSteps(mi, steps);

            for (int j = 0; j < steps; j++)
            {
                DoMovement(mi, stepSize);
            }
        }
    }

    private Dictionary<MI, int> debugSteps;

    private void AddDebugSteps(MI mi, int steps)
    {
        if (debugSteps == null) { ResetDebugSteps(); }

        if (debugSteps.ContainsKey(mi))
        {
            int currentSteps = debugSteps[mi];
            currentSteps += steps;
            debugSteps[mi] = currentSteps;
        }
        else
        {
            debugSteps.Add(mi, steps);
        }
    }

    public Dictionary<MI, int>  GetDebugSteps()
    {
        if (debugSteps == null) { ResetDebugSteps(); }
        return debugSteps;
    }

    public void ResetDebugSteps()
    {
        debugSteps = new Dictionary<MI, int>();
    }

}
