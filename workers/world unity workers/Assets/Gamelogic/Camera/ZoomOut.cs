using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour {

    [SerializeField] private float speed = 2;

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update () {
        if (Input.GetKey(KeyCode.F2))
        {
            cam.orthographicSize += speed * Time.deltaTime;
        }
	}
}
