using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    [SerializeField] GameObject birdPrefab;
    public List<BirdLandingSpot> birdLandingSpots;
    [HideInInspector] public List<BirdBehavior> birds;

    private List<BirdLandingSpot> UnoccupiedSpots;

    private void Awake()
    {
        birds = new List<BirdBehavior>();
        UnoccupiedSpots = new List<BirdLandingSpot>(birdLandingSpots);
        UpdateListOfLandingSpots();
    }

    private void Start()
    {
        SpawnBirds();
    }

    public void UpdateListOfLandingSpots()
    {
        birdLandingSpots = new List<BirdLandingSpot>(FindObjectsOfType<BirdLandingSpot>());
    }

    private void SpawnBirds()
    {
        GameObject bird;

        for (int i = 0; i < birdLandingSpots.Count; i++)
        {
            if (birdLandingSpots[i].birdToSpawn)
            {
                bird = Instantiate(birdPrefab, birdLandingSpots[i].transform.position, birdLandingSpots[i].transform.rotation);
                BirdBehavior birdBehavior = bird.GetComponent<BirdBehavior>();
                birds.Add(birdBehavior);
                birdBehavior.currentLandingSpot = birdLandingSpots[i];
                birdBehavior.SetColor(birdLandingSpots[i].birdToSpawn.birdMaterial);
                birdBehavior.treePreference = birdLandingSpots[i].birdToSpawn.treePreference;

                UnoccupiedSpots.Remove(birdLandingSpots[i]);
            }
        }
    }


    public BirdLandingSpot GetNewLandingSpot(TreeType desiredTreeType)
    {
        for (int i = 0; i < UnoccupiedSpots.Count; i++)
        {
            if (UnoccupiedSpots[i].GetTreeType() == desiredTreeType && !UnoccupiedSpots[i].inFrustum)
            {
                BirdLandingSpot toReturn = UnoccupiedSpots[i];
                UnoccupiedSpots.Remove(UnoccupiedSpots[i]);
                return toReturn;
            }
        }
        return null;
    }

    public BirdLandingSpot GetNewLandingSpot()
    {
        for (int i = 0; i < UnoccupiedSpots.Count; i++)
        {
            if (!UnoccupiedSpots[i].inFrustum)
            {
                BirdLandingSpot toReturn = UnoccupiedSpots[i];
                UnoccupiedSpots.Remove(UnoccupiedSpots[i]);
                return toReturn;
            }
        }
        return null;
    }

    public void LeaveLandingSpot(BirdLandingSpot spot)
    {
        UnoccupiedSpots.Add(spot);
    }


}
