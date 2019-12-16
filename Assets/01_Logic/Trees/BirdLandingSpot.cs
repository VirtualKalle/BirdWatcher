using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLandingSpot : MonoBehaviour
{
    public bool inFrustum = false;
    public BirdTypeScriptableObject birdToSpawn;
    [SerializeField] MeshRenderer meshRenderer;
    
    private void Start()
    {
        meshRenderer.enabled = false;
    }

    public TreeType GetTreeType()
    {
        return GetComponentInParent<TreeBehavior>().GetTreeType();
    }

}
