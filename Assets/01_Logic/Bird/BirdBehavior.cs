using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    enum BirdState { Idle, Spotted, Flying }
    enum TreePreference { Leaf, Any }

    [SerializeField] TreePreference treePreference;
    [SerializeField] BirdState birdState = BirdState.Idle;

    private Animator animator;
    private bool spottedCoolDown = false;
    public float flightProgress;
    Transform startTransform;
    Transform finishTransform;
    BirdManager birdManager;
    public BirdLandingSpot currentLandingSpot;
    public BirdLandingSpot targetLandingSpot;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        birdManager = FindObjectOfType<BirdManager>();
    }

    private void Start()
    {
    }


    public void SpottedByGaze()
    {
        startTransform = currentLandingSpot.transform;
        if (!spottedCoolDown && birdState == BirdState.Idle)
        {
            birdState = BirdState.Spotted;
            animator.SetInteger("birdState", (int)birdState);
        }
    }

    private void LandedInNewTree()
    {
        birdState = BirdState.Idle;
        animator.SetInteger("birdState", (int)birdState);
        spottedCoolDown = true;

        currentLandingSpot = targetLandingSpot;
        startTransform = currentLandingSpot.transform;

        StartCoroutine(CountDownResetSpotted());


    }

    private IEnumerator CountDownResetSpotted(float countDownTime = 3)
    {
        float timeLeft = countDownTime;
        while (timeLeft > 0)
        {
            Debug.Log("Countdown: " + timeLeft);
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }
        spottedCoolDown = false;
    }

    public void FlyToNextSpot()
    {
        switch (treePreference)
        {
            case TreePreference.Leaf:
                targetLandingSpot = birdManager.GetNewLandingSpot(TreeType.Leaves);
                break;
            case TreePreference.Any:
                targetLandingSpot = birdManager.GetNewLandingSpot();
                break;
            default:
                break;
        }

        if (targetLandingSpot)
        {
            birdManager.LeaveLandinSpot(currentLandingSpot);

            finishTransform = targetLandingSpot.transform;

            birdState = BirdState.Flying;
            animator.SetInteger("birdState", (int)birdState);
            StartCoroutine(ILerpRotation());

            Debug.Log("birdState BirdState.Flying");
            StartCoroutine(ILerpPosition());
        }
        else
        {
            birdState = BirdState.Idle;
            animator.SetInteger("birdState", (int)birdState);
        }

    }


    IEnumerator ILerpPosition()
    {
        Debug.Log("start lerp pos");
        float startDistance = Vector3.Distance(startTransform.position, finishTransform.position);

        while (flightProgress < 1f)
        {
            transform.position = startTransform.position + (finishTransform.position - startTransform.position) * flightProgress;
            transform.position = new Vector3(transform.position.x, startTransform.position.y + 0.5f * (-Mathf.Pow(flightProgress * 2 - 1f, 2) + 1f), transform.position.z);
            yield return null;
        }
        Debug.Log("finish lerp pos");
    }

    IEnumerator ILerpRotation()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(finishTransform.position - startTransform.position);

        while (Quaternion.Angle(targetRotation, transform.rotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1);
            yield return new WaitForSeconds(.01f);
        }
        Debug.Log("finish lerp pos");

    }

}
