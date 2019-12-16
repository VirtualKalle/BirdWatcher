using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreeBehavior : MonoBehaviour
{
    [SerializeField] private TreeType treeType = TreeType.Leaves;

    public TreeType GetTreeType()
    {
        return treeType;
    }

}
