using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BirdType", order = 1)]
public class BirdTypeScriptableObject : ScriptableObject
{
    public TreePreference treePreference;
    public Material birdMaterial;
}