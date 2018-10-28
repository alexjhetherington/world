using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    private GameObject target;
    private Camera cam;

    //Distance character can move before camera moves
    [SerializeField] private float verticalWalkDistance;
    [SerializeField] private float horizontalWalkDistance;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        HideEverything();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(target != null)
        {
            Vector3 newPos = cam.transform.position;

            //X is horizontal
            float horizontalDistance = target.transform.position.x - cam.transform.position.x;
            //Z is vertical
            float verticalDistance = target.transform.position.z - cam.transform.position.z;

            if (Mathf.Abs(horizontalDistance) > horizontalWalkDistance)
            {
                newPos.x = target.transform.position.x - (Mathf.Sign(horizontalDistance) * horizontalWalkDistance);
            }

            if (Mathf.Abs(verticalDistance) > verticalWalkDistance)
            {
                newPos.z = target.transform.position.z - (Mathf.Sign(verticalDistance) * verticalWalkDistance);
            }


            cam.transform.position = newPos;
        }
	}

    private void CenterCamera()
    {
        cam.transform.localPosition = new Vector3(0, 10, 0);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
        CenterCamera();
        if(target == null)
        {
            HideEverything();
        }
        else
        {
            ShowEverything();
        }
    }

    private void HideEverything()
    {
        cam.cullingMask = 0;
    }

    private void ShowEverything()
    {
        cam.cullingMask = -1;
    }
}
