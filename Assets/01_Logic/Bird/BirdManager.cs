using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

    [SerializeField] BirdLandingSpot[] birdLandingSpots;
    [SerializeField] GameObject birdPrefab;
    public List<BirdBehavior> birds;


    // Start is called before the first frame update
    void Start()
    {
        UpdateLandingSpots();
        SpawnBirds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLandingSpots()
    {
        birdLandingSpots = FindObjectsOfType<BirdLandingSpot>();
    }

    private void SpawnBirds(float ratio = 1f)
    {

        int nrOfBirdsToSpawn = Mathf.Max(1, (int)(birdLandingSpots.Length * ratio));

        //Create a randomized queue of landingspots

        GameObject bird;
        for (int i = 0; i < nrOfBirdsToSpawn; i++)
        {
            bird = Instantiate(birdPrefab, birdLandingSpots[i].transform.position, birdLandingSpots[i].transform.rotation);
            birds.Add(bird.GetComponent<BirdBehavior>());
        }
    }

}
