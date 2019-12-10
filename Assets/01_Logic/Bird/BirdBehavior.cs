using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehavior : MonoBehaviour
{
    private Animator animator;
    private bool spottedCoolDown = false;



    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        if (!spottedCoolDown)
        {
            animator.SetBool("isSpotted", true);
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

    void FlyToNextSpot()
    {

    }
}
