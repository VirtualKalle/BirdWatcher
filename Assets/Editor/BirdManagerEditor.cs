using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BirdManager))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BirdManager birdManager = (BirdManager)target;

        if (GUILayout.Button("UpdateLandingSpots"))
        {
            birdManager.UpdateLandingSpots();
        }
    }
}