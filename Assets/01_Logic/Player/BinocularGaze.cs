using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularGaze : MonoBehaviour
{

    [SerializeField] BirdManager birdManager;
    [SerializeField] Transform FocusPoint_L;
    [SerializeField] Transform FocusPoint_R;
    [SerializeField] Camera cam;

    private List<BirdBehavior> birds;
    private List<BirdLandingSpot> landingSpots;

    public float maxAngleToDetectBird = 25;

    private void Awake()
    {
        if (!birdManager)
        {
            birdManager = FindObjectOfType<BirdManager>();
        }

        birds = new List<BirdBehavior>();
        landingSpots = new List<BirdLandingSpot>();

        if (!cam)
        {
            cam = Camera.main;
        }
    }

    private void Start()
    {
        landingSpots = birdManager.birdLandingSpots;
        birds = birdManager.birds;
    }


    void Update()
    {
        GazeForLandingSpots();
        GazeForBirds();
    }

    private void OnDrawGizmos()
    {
        Matrix4x4 initialMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(cam.transform.position, cam.transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, cam.fieldOfView, cam.farClipPlane, cam.nearClipPlane, cam.aspect);

        Gizmos.matrix = initialMatrix;
    }


    void GazeForBirds()
    {
        Vector3 focusPointPos_L = FocusPoint_L.position;
        Vector3 focusPointPos_R = FocusPoint_R.position;
        Vector3 camPos = cam.transform.position;
        
        for (int i = 0; i < birds.Count; i++)
        {
            float angle_L = Mathf.Acos(Vector3.Dot((focusPointPos_L - camPos).normalized, (birds[i].transform.position - camPos).normalized)) * Mathf.Rad2Deg;
            float angle_R = Mathf.Acos(Vector3.Dot((focusPointPos_R - camPos).normalized, (birds[i].transform.position - camPos).normalized)) * Mathf.Rad2Deg;

            if (angle_L < maxAngleToDetectBird || angle_R < maxAngleToDetectBird)
            {
                birds[i].SpottedByGaze();
            }
        }
    }


    void GazeForLandingSpots()
    {
        Vector3 focusPointPos_L = FocusPoint_L.position;
        Vector3 focusPointPos_R = FocusPoint_R.position;
        Vector3 camPos = cam.transform.position;

        for (int i = 0; i < landingSpots.Count; i++)
        {
            float angle_L = Mathf.Acos(Vector3.Dot((focusPointPos_L - camPos).normalized, (landingSpots[i].transform.position - camPos).normalized)) * Mathf.Rad2Deg;
            float angle_R = Mathf.Acos(Vector3.Dot((focusPointPos_R - camPos).normalized, (landingSpots[i].transform.position - camPos).normalized)) * Mathf.Rad2Deg;

            if (angle_L < maxAngleToDetectBird || angle_R < maxAngleToDetectBird)
            {
                landingSpots[i].inFrustum = true;
            }
            else
            {
                landingSpots[i].inFrustum = false;
            }
        }
    }


    // TODO: Build a generic for Gaze using interface Spotted(), Hidden()
    /*
    void Gaze<T>(List<T> objList)
    {
        Vector3 focusPointPos_L = FocusPoint_L.position;
        Vector3 focusPointPos_R = FocusPoint_R.position;
        Vector3 camPos = cam.transform.position;

        //List<Transform> transforms = objList as List<Transform>;
        for (int i = 0; i < objList.Count; i++)
        {
            float angle_L = Mathf.Acos(Vector3.Dot((focusPointPos_L - camPos).normalized, (objList[i].transform.position - camPos).normalized)) * Mathf.Rad2Deg;
            float angle_R = Mathf.Acos(Vector3.Dot((focusPointPos_R - camPos).normalized, (objList[i].transform.position - camPos).normalized)) * Mathf.Rad2Deg;
            
            if (angle_L < maxAngleToDetectBird || angle_R < maxAngleToDetectBird)
            {
                objList[i].Spotted()
            }
            else
            {
                objList[i].Hidden()
            }  
        }


    }
    */

}
