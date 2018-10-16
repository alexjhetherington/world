using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputs : MovementInput {
    public readonly float horizontalAxis;
    public readonly float verticalAxis;
    public readonly bool spiritMode;

    public CharacterInputs(float timestamp, float horizontalAxis, float verticalAxis, bool spiritMode) : base(timestamp)
    {
        this.horizontalAxis = horizontalAxis;
        this.verticalAxis = verticalAxis;
        this.spiritMode = spiritMode;
    }

    public override bool IsNew(MovementInput other)
    {
        CharacterInputs charIn = other as CharacterInputs;
        if(horizontalAxis.Equals(charIn.horizontalAxis) && verticalAxis.Equals(charIn.verticalAxis) && spiritMode == charIn.spiritMode)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public override bool IsNone()
    {
        return horizontalAxis == 0 && verticalAxis == 0;
    }

    public override string ToString()
    {
        return "Horizontal: " + horizontalAxis + ", Vertical: " + verticalAxis + ", spiritMode: " + spiritMode + ", timestamp: " + timestamp;
    }
}
