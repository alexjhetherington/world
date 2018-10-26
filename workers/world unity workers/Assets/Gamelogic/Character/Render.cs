using Improbable.Character;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityClient)]
public class Render : MonoBehaviour {

    [Require] private CharacterControls.Reader characterControlsReader;
    private OtherCharacterClientMovement otherCharacterClientMovement = null;

    private ChatPrep chatPrep;

    [SerializeField] private MeshRenderer normalStill;
    [SerializeField] private MeshRenderer spiritStill;
    [SerializeField] private MeshRenderer normalDirection;
    [SerializeField] private MeshRenderer spiritDirection;

    private bool isSpirit = false;

    enum Facing { None, East, SouthEast, South, SouthWest, West, NorthWest, North, NorthEast };

    private Facing facing = Facing.None;

    private void Awake()
    {
        spiritStill.enabled = false;
        normalDirection.enabled = false;
        spiritDirection.enabled = false;

        chatPrep = GetComponent<ChatPrep>();
    }

    private void OnEnable()
    {
        if (characterControlsReader.Authority != Improbable.Worker.Authority.Authoritative)
        {
            otherCharacterClientMovement = GetComponent<OtherCharacterClientMovement>();
        }
    }

    // Update is called once per frame
    void Update () {
        CharacterInputs charInputs;
        if (characterControlsReader.Authority == Improbable.Worker.Authority.Authoritative)
        {
            if (!chatPrep.IsTyping())
            {
                charInputs = new CharacterInputs(0, Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetKey(KeyCode.LeftShift));
            }
            else
            {
                charInputs = new CharacterInputs(0, 0, 0, false);
            }
        }
        else
        {
            MovementInput input = otherCharacterClientMovement.GetCurrentInput();
            if (input != null)
            {
                charInputs = (CharacterInputs)input;
            }
            else
            {
                charInputs = new CharacterInputs(0, 0, 0, false);
            }
        }

        bool newSpirit = GetSpirit(charInputs);
        bool spiritChanged = newSpirit != isSpirit;
        if (spiritChanged)
        {
            SetSpirit(newSpirit);
        }

        Facing newFacing = GetFacing(charInputs);
        if(newFacing != facing || spiritChanged)
        {
            SetFacing(newFacing);
        }
    }

    private Facing GetFacing(CharacterInputs charInputs)
    {
        var horizontalAxis = charInputs.horizontalAxis;
        var verticalAxis = charInputs.verticalAxis;

        if (verticalAxis > 0)
        {
            if (horizontalAxis > 0)
            {
                return Facing.NorthEast;
            }
            else if (horizontalAxis == 0)
            {
                return Facing.North;
            }
            else
            {
                return Facing.NorthWest;
            }
        }
        else if (verticalAxis == 0)
        {
            if (horizontalAxis > 0)
            {
                return Facing.East;
            }
            else if (horizontalAxis == 0)
            {
                return Facing.None;
            }
            else
            {
                return Facing.West;
            }
        }
        else
        {
            if (horizontalAxis > 0)
            {
                return Facing.SouthEast;
            }
            else if (horizontalAxis == 0)
            {
                return Facing.South;
            }
            else
            {
                return Facing.SouthWest;
            }
        }
    }

    private void SetFacing(Facing facing)
    {
        this.facing = facing;
        GameObject enabledObject = EnableAndGetCorrectObject();
        float eulerRotation = 0;

        //Set rotation based on cardinal direction
        switch (facing)
        {
            case (Facing.None):
                eulerRotation = 0;
                break;
            case (Facing.North):
                eulerRotation = -90;
                break;
            case (Facing.NorthEast):
                eulerRotation = -45;
                break;
            case (Facing.East):
                eulerRotation = 0;
                break;
            case (Facing.SouthEast):
                eulerRotation = 45;
                break;
            case (Facing.South):
                eulerRotation = 90;
                break;
            case (Facing.SouthWest):
                eulerRotation = 135;
                break;
            case (Facing.West):
                eulerRotation = 180;
                break;
            case (Facing.NorthWest):
                eulerRotation = -135;
                break;
        }

        enabledObject.transform.localEulerAngles = new Vector3(90, eulerRotation, 0);
    }

    private GameObject EnableAndGetCorrectObject()
    {
        if (isSpirit && facing == Facing.None)
        {
            normalStill.enabled = false;
            spiritStill.enabled = true;
            normalDirection.enabled = false;
            spiritDirection.enabled = false;
            return spiritStill.gameObject;
        }

        if (!isSpirit && facing == Facing.None)
        {
            normalStill.enabled = true;
            spiritStill.enabled = false;
            normalDirection.enabled = false;
            spiritDirection.enabled = false;
            return normalStill.gameObject;
        }

        if (isSpirit && facing != Facing.None)
        {
            normalStill.enabled = false;
            spiritStill.enabled = false;
            normalDirection.enabled = false;
            spiritDirection.enabled = true;
            return spiritDirection.gameObject;
        }

        if (!isSpirit && facing != Facing.None)
        {
            normalStill.enabled = false;
            spiritStill.enabled = false;
            normalDirection.enabled = true;
            spiritDirection.enabled = false;
            return normalDirection.gameObject;
        }

        return null;
    }

    private bool GetSpirit(CharacterInputs charInputs)
    {
        if (gameObject.layer == 11 || charInputs.spiritMode)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetSpirit(bool isSpirit)
    {
        this.isSpirit = isSpirit;
        EnableAndGetCorrectObject();
    }
}
