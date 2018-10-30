using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCalc : MovementCalculation<CharacterInputs> {

    protected CharacterController characterController;

    private float normalSpeed = 2;
    private float spiritSpeed = 5;

    private bool isStill = true;

    public MoveCalc(CharacterController characterController)
    {
        this.characterController = characterController;
    }
    
    protected override void DoMovement(CharacterInputs movementInput, float deltaTime)
    {
        if (movementInput.spiritMode)
        {
            characterController.gameObject.layer = 11;
        }
        else
        {
            characterController.gameObject.layer = 9;
        }

        //Not quite correct for capsule collider of character controller but we are only worried about the 2d plane
        int mask = ~((1 << 9) | (1 << 11));
        Collider[] overlapping = Physics.OverlapSphere(new Vector3(characterController.transform.position.x, 0, characterController.transform.position.z), characterController.radius, mask);
        if (overlapping.Length > 0)
        {
            characterController.gameObject.layer = 11;
        }

        var horizontalAxis = movementInput.horizontalAxis;
        var verticalAxis = movementInput.verticalAxis;

        isStill = verticalAxis == 0 && horizontalAxis == 0;

        Vector2 direction = new Vector2(horizontalAxis, verticalAxis);
            direction = direction.normalized;

        Vector2 vector;
        if (!movementInput.spiritMode)
        {
            vector = direction* deltaTime * normalSpeed;
        }
        else
        {
            vector = direction * deltaTime * spiritSpeed;
        }

        characterController.Move(new Vector3(vector.x, 0, vector.y));
    }

    public override bool IsStill()
    {
        return isStill;
    }
}
