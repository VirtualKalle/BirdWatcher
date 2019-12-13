using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VisionGizmo : MonoBehaviour
{
    [SerializeField] Transform FocusPoint;
    [SerializeField] float baseScale;
    private BinocularGaze binocularGaze;

    private void Awake()
    {
        binocularGaze = GetComponentInParent<BinocularGaze>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(FocusPoint);
        AdjustCone();
    }

    void AdjustCone()
    {
        float angle = binocularGaze.maxAngleToDetectBird;
        transform.localScale = new Vector3(baseScale * angle / 30, baseScale * angle / 30, baseScale);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(FocusPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        binocularGaze.OnTriggerEnterVisionCone(other);
    }

    private void OnTriggerExit(Collider other)
    {
        binocularGaze.OnTriggerExitVisionCone(other);
    }


}
