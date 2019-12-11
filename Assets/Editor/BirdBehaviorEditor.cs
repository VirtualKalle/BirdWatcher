using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BirdBehavior))]
public class BirdBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BirdBehavior birdBehavior = (BirdBehavior)target;

        if (GUILayout.Button("FlyToNextSpot"))
        {
            birdBehavior.FlyToNextSpot();
        }
    }
}