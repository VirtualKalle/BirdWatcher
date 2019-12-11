using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BirdManager))]
public class BirdManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BirdManager birdManager = (BirdManager)target;

        if (GUILayout.Button("UpdateLandingSpots"))
        {
            birdManager.UpdateListOfLandingSpots();
        }
    }
}