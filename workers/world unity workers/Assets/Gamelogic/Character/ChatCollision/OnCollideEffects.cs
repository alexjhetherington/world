using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideEffects : MonoBehaviour {

    void OnCollisionEnter(Collision collisionInfo)
    {
        //Debug.Log("Now in contact with " + collisionInfo.transform.name);
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        //Debug.Log("No longer in contact with " + collisionInfo.transform.name);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        //Debug.Log("Touching " + collisionInfo.transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger entered " + collisionInfo.transform.name);
        //gameObject.layer = 11;
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Trigger exited: " + other.transform.name);
    }

    // Applies an upwards force to all rigidbodies that enter the trigger.
    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger stayed: " + other.transform.name);
    }
}
