using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VisionGizmo : MonoBehaviour
{
    [SerializeField] Transform FocusPoint;
    [SerializeField] float baseScale = 500;

    private BinocularGaze binocularGaze;

    private void Awake()
    {
        binocularGaze = GetComponentInParent<BinocularGaze>();
    }


    private void Start()
    {
        transform.LookAt(FocusPoint);
        AdjustCone();
    }

    private void AdjustCone()
    {
        float angle = binocularGaze.maxAngleToDetectBird;
        transform.localScale = new Vector3(baseScale * angle / 30, baseScale * angle / 30, baseScale);
    }

    private void Update()
    {
        transform.LookAt(FocusPoint);
    }

}
