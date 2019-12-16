using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdBehavior : MonoBehaviour
{

    public TreePreference treePreference = TreePreference.Any;
    public float flightProgress;
    public BirdLandingSpot currentLandingSpot;
    public BirdLandingSpot targetLandingSpot;

    [SerializeField] BirdState birdState = BirdState.Idle;
    [SerializeField] List<MeshRenderer> bodyMainColor;

    private enum BirdState { Idle, Spotted, Flying }
    private bool spottedCoolDown = false;
    private Animator animator;
    private BirdManager birdManager;
    private LineRenderer lineRenderer;
    private Transform startTransform;
    private Transform finishTransform;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        birdManager = FindObjectOfType<BirdManager>();
    }

    private void Start()
    {
        startTransform = currentLandingSpot.transform;
    }

    public void SetColor(Material material)
    {
        for (int i = 0; i < bodyMainColor.Count; i++)
        {
            bodyMainColor[i].material = material;
        }
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
            birdManager.LeaveLandingSpot(currentLandingSpot);

            finishTransform = targetLandingSpot.transform;
            DrawFlightPath();
            birdState = BirdState.Flying;
            animator.SetInteger("birdState", (int)birdState);

            StartCoroutine(ILerpRotation());
            StartCoroutine(ILerpPosition());
        }
        else
        {
            birdState = BirdState.Idle;
            animator.SetInteger("birdState", (int)birdState);
            spottedCoolDown = true;
            StartCoroutine(CountDownResetSpotted());
        }

    }

    private void DrawFlightPath()
    {
        int drawResolution = 100;
        Vector3[] positions = new Vector3[drawResolution];
        Vector3 flightPoint;
        Vector3 startPos = startTransform.position;
        Vector3 finishPos = finishTransform.position;
        float offset = 0.06f;

        for (int i = 0; i < drawResolution; i++)
        {
            flightPoint = startPos + (finishPos - startPos) * i / drawResolution;
            flightPoint.y += 0.5f * (-Mathf.Pow((float)i / drawResolution * 2 - 1f, 2) + 1f) - offset;
            positions[i] = flightPoint;
        }

        lineRenderer.positionCount = drawResolution;
        lineRenderer.SetPositions(positions);
        lineRenderer.enabled = true;
    }

    private void LandedInNewTree()
    {
        birdState = BirdState.Idle;
        animator.SetInteger("birdState", (int)birdState);
        currentLandingSpot = targetLandingSpot;
        startTransform = currentLandingSpot.transform;
        lineRenderer.enabled = false;
        spottedCoolDown = true;
        StartCoroutine(CountDownResetSpotted());
    }


    private IEnumerator CountDownResetSpotted(float countDownTime = 3)
    {
        float timeLeft = countDownTime;
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }
        spottedCoolDown = false;
    }

    private IEnumerator ILerpPosition()
    {
        float startDistance = Vector3.Distance(startTransform.position, finishTransform.position);

        Vector3 flightPos;
        Vector3 startPos = startTransform.position;
        Vector3 finishPos = finishTransform.position;

        while (flightProgress < 1f)
        {
            flightPos = startPos + (finishPos - startPos) * flightProgress;
            flightPos.y += 0.5f * (-Mathf.Pow(flightProgress * 2 - 1f, 2) + 1f);
            transform.position = flightPos;
            yield return null;
        }
    }

    private IEnumerator ILerpRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(finishTransform.position - startTransform.position);

        while (Quaternion.Angle(targetRotation, transform.rotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1);
            yield return new WaitForSeconds(.01f);
        }

    }





}
