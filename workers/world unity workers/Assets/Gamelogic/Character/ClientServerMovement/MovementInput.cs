using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementInput {
    public readonly float timestamp;

    public MovementInput(float timestamp)
    {
        this.timestamp = timestamp;
    }

    public abstract bool IsNew(MovementInput other);
    public abstract bool IsNone();
}
