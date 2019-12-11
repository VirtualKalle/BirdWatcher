using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{

    [SerializeField] BirdLandingSpot[] birdLandingSpots;
    List<BirdLandingSpot> UnoccupiedSpots;
    [SerializeField] GameObject birdPrefab;
    public List<BirdBehavior> birds;

    private void Awake()
    {
        UpdateListOfLandingSpots();
        UnoccupiedSpots = new List<BirdLandingSpot>(birdLandingSpots);
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

    public BirdLandingSpot GetNewLandingSpot(TreeType desiredTreeType)
    {
        for (int i = 0; i < UnoccupiedSpots.Count; i++)
        {
            if (UnoccupiedSpots[i].GetTreeType() == desiredTreeType)
            {
                Debug.Log("found " + UnoccupiedSpots[i]);
                BirdLandingSpot toReturn = UnoccupiedSpots[i];
                UnoccupiedSpots.Remove(UnoccupiedSpots[i]);
                return toReturn;
            }
        }
        Debug.Log("didnt find " + desiredTreeType);
        return null;
    }

    public BirdLandingSpot GetNewLandingSpot()
    {
        TreeType desiredTreeType = (TreeType)Random.Range(0, 1);
        return GetNewLandingSpot(desiredTreeType);
    }

    public void LeaveLandinSpot(BirdLandingSpot spot)
    {
        UnoccupiedSpots.Add(spot);
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
            bird.GetComponent<BirdBehavior>().currentLandingSpot = birdLandingSpots[i];
            UnoccupiedSpots.Remove(birdLandingSpots[i]);
        }
    }

}
