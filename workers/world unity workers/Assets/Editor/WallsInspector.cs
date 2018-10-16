using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Walls))]
public class WallsInspector : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Walls walls = (Walls)target;
        if (GUILayout.Button("Set Walls"))
        {
            walls.SizeWalls();
        }
    }
}
