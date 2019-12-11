using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    enum BirdState {Idle, Spotted, Flying }
    [SerializeField] BirdState birdState = BirdState.Idle;

    private Animator animator;
    private bool spottedCoolDown = false;
    public float flightProgress;
    Transform startTransform;
    Transform finishTransform;
    BirdManager birdManager;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        birdManager = FindObjectOfType<BirdManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpottedByGaze()
    {
        if (!spottedCoolDown && birdState == BirdState.Idle)
        {
            birdState = BirdState.Spotted;
            animator.SetInteger("birdState", (int) birdState);
        }
    }

    private void LandedInNewTree()
    {
        animator.SetBool("isSpotted", false);
        spottedCoolDown = true;
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

        startTransform = transform;
        finishTransform = birdManager.GetLandingSpot(TreeType.Plain);

        birdState = BirdState.Flying;
        animator.SetInteger("birdState", (int)birdState);

        Debug.Log("birdState BirdState.Flying");
        StartCoroutine(ILerpPosition());
    }

    void SetNextSpot(Transform nextSpotTransform)
    {
    }

    IEnumerator ILerpPosition()
    {
        Debug.Log("start lerp pos");
        float startDistance = Vector3.Distance(startTransform.position, finishTransform.position);

        while (flightProgress < 1f)
        {
            transform.position = Vector3.Lerp(startTransform.position, finishTransform.position, flightProgress);
            yield return null;
        }
        Debug.Log("finish lerp pos");
    }

    void LerpPosition()
    {
        Debug.Log("start lerp pos");
        float startDistance = Vector3.Distance(startTransform.position, finishTransform.position);

        transform.position = Vector3.Lerp(startTransform.position, finishTransform.position, flightProgress * startDistance);

        Debug.Log("finish lerp pos");
    }
}
