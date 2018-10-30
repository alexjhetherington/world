using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChange : MonoBehaviour {

    private Camera cam;

	// Use this for initialization
	void Awake () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float colourChangeSpeed = 0.05f;

        float xLoop1 =
            Mathf.Cos(transform.position.x * colourChangeSpeed);
        float zLoop1 =
            Mathf.Cos(transform.position.z * colourChangeSpeed);
        
        Color background = new Color(1, xLoop1, zLoop1);

        cam.backgroundColor = background;
	}
}
