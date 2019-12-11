using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

    [SerializeField] BirdLandingSpot[] birdLandingSpots;
    [SerializeField] GameObject birdPrefab;
    public List<BirdBehavior> birds;

    private void Awake()
    {
        UpdateListOfLandingSpots();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnBirds(0.5f);
    }

    public void UpdateListOfLandingSpots()
    {
        birdLandingSpots = FindObjectsOfType<BirdLandingSpot>();
    }

    public Transform GetLandingSpot(TreeType desiredTreeType)
    {
        for (int i = 0; i < birdLandingSpots.Length; i++)
        {
            if (birdLandingSpots[i].GetTreeType() == desiredTreeType)
            {
                return birdLandingSpots[i].transform;
            }
        }

        return null;
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
