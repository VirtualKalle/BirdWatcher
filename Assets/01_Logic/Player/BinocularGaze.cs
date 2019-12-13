using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularGaze : MonoBehaviour
{

    [SerializeField] BirdManager birdManager;
    [SerializeField] Transform FocusPoint_L;
    [SerializeField] Transform FocusPoint_R;


    [SerializeField] MeshCollider VisionCone_L;
    [SerializeField] MeshCollider VisionCone_R;

    List<BirdBehavior> spottedBirds;
    private List<BirdBehavior> birds;
    Camera cam;
    Plane[] planes;

    public float maxAngleToDetectBird { get; private set; } = 20;

    private void Awake()
    {
        spottedBirds = new List<BirdBehavior>();
        if (!birdManager)
        {
            birdManager = FindObjectOfType<BirdManager>();
        }

        birds = birdManager.birds;

        cam = Camera.main;
    }


    void Update()
    {
        SpottedBirds();
    }


    void VectorDotGaze(Transform focusPoint)
    {
        for (int i = 0; i < birds.Count; i++)
        {
            float angle = Mathf.Acos(Vector3.Dot((focusPoint.position - cam.transform.position).normalized, (birds[i].transform.position - cam.transform.position).normalized)) * Mathf.Rad2Deg;

            if (angle < maxAngleToDetectBird)
            {
                birds[i].SpottedByGaze();
            }
        }
    }

    public void OnTriggerEnterVisionCone(Collider other)
    {

        BirdBehavior bird = other.GetComponentInParent<BirdBehavior>();

        if (bird)
        {
            spottedBirds.Add(bird);
        }
    }

    public void OnTriggerExitVisionCone(Collider other)
    {

        BirdBehavior bird = other.GetComponentInParent<BirdBehavior>();

        if (bird)
        {
            spottedBirds.Remove(bird);
        }
    }


    void SpottedBirds()
    {
        for (int i = 0; i < spottedBirds.Count; i++)
        {
            spottedBirds[i].SpottedByGaze();
        }
    }

}
