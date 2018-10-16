using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthoritativeTransform {

    public readonly Vector3 position;
    public readonly Quaternion rotation;
    public readonly float timestamp;

    public AuthoritativeTransform(float timestamp, Vector3 position, Quaternion rotation)
    {
        this.timestamp = timestamp;
        this.position = position;
        this.rotation = rotation;
    }

    public override string ToString()
    {
        return "Timestamp: " + timestamp + ", position: " + position;
    }
}
