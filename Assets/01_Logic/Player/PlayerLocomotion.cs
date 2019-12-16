using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    private Camera mainCamera;
    private bool startedRotation;
    private Vector3 lastMousePosition;
    [SerializeField] float sensitivity = 1;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            RotateCamera();
            lastMousePosition = Input.mousePosition;
        }

    }

    void RotateCamera()
    {
        Vector3 deltaPos = lastMousePosition - Input.mousePosition;

        mainCamera.transform.Rotate(0, -deltaPos.x * sensitivity, 0, Space.World);
        mainCamera.transform.Rotate(deltaPos.y * sensitivity, 0, 0, Space.Self);
    }

}
