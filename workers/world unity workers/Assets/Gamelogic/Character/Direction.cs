using Improbable.Character;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO investigate - Do the sprites have a performance cost on the server?

//TODO angle based on direction - means joystick type controls will work and get rid of this horrible if statement

//TODO basically just refactor this whole thing
[WorkerType(WorkerPlatform.UnityClient)]
public class Direction : MonoBehaviour {

    [Require]
    private CharacterControls.Writer characterControlsWriter; //TODO can use client authority

    public SpriteRenderer directionSprite;

    private ChatPrep chatPrep;

    private void Awake()
    {
        chatPrep = GetComponent<ChatPrep>();
    }

    private void OnEnable()
    {
        directionSprite.gameObject.SetActive(true);
        directionSprite.enabled = false;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        var horizontalAxis = Input.GetAxisRaw("Horizontal");
        var verticalAxis = Input.GetAxisRaw("Vertical");

        if (chatPrep.IsTyping())
        {
            horizontalAxis = 0;
            verticalAxis = 0;
        }
        
        if (verticalAxis > 0)
        {
            if(horizontalAxis > 0)
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, 45, 0);
            }
            else if (horizontalAxis == 0)
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, 0, 0);
            }
            else
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, -45, 0);
            }
        }
        else if(verticalAxis == 0)
        {
            if (horizontalAxis > 0)
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, 90, 0);
            }
            else if (horizontalAxis == 0)
            {
                directionSprite.enabled = false;
                return;
            }
            else
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, -90, 0);
            }
        }
        else
        {
            if (horizontalAxis > 0)
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, 135, 0);
            }
            else if (horizontalAxis == 0)
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, 180, 0);
            }
            else
            {
                directionSprite.transform.localEulerAngles = new Vector3(90, -135, 0);
            }
        }

        directionSprite.enabled = true;
    }
}
