using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularGaze : MonoBehaviour
{

    [SerializeField] BirdManager birdManager;
    [SerializeField] Transform FocusPoint_L;
    [SerializeField] Transform FocusPoint_R;

    private List<BirdBehavior> birds;
    Camera cam;
    Plane[] planes;

    private void Awake()
    {
        if (!birdManager)
        {
            birdManager = FindObjectOfType<BirdManager>();
        }

        birds = birdManager.birds;

        cam = Camera.main;
    }
    

    void Update()
    {

        VectorDotGaze(FocusPoint_L);
        VectorDotGaze(FocusPoint_R);
        //FrustumPlaneGaze();

    }


    void VectorDotGaze(Transform focusPoint)
    {
        for (int i = 0; i < birds.Count; i++)
        {
            float angle = Mathf.Acos(Vector3.Dot((focusPoint.position - cam.transform.position).normalized, (birds[i].transform.position - cam.transform.position).normalized)) * Mathf.Rad2Deg;
            //Debug.Log(birds[i].transform.position);
            //Debug.Log(angle);

            if (angle < 10)
            {
                birds[i].SpottedByGaze();
            }
        }
    }


}
