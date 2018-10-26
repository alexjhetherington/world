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
            //Debug.Log("Overlapping: " + overlapping.Length);
            characterController.gameObject.layer = 11;
        }

        //Vector3 startPosition = characterController.gameObject.transform.transform.position;

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
        //Debug.Log(vector.magnitude);

        characterController.Move(new Vector3(vector.x, 0, vector.y)); //TODO this is appropriate for server side movement?

        //Vector3 endPosition = characterController.gameObject.transform.transform.position;

        /*float distance = (endPosition - startPosition).magnitude;
        Debug.Log("Distance: " + distance);
        float timeMoved = deltaTime;
        Debug.Log("Simulated time: " + deltaTime);
        Debug.Log("Simulated speed: " + distance / timeMoved);*/
    }

    public override bool IsStill()
    {
        return isStill;
    }
}
