using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLandingSpot : MonoBehaviour
{
    public bool occupied { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TreeType GetTreeType()
    {
        return GetComponentInParent<TreeBehavior>().GetTreeType();
    }
}
