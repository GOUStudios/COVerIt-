using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesOutUI : MonoBehaviour
{
    private PointsManager pointsManager;

    [SerializeField] private Animator endLevelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TimerManagerMonoBehaviour.OnTimeFinished += TimesOverBehavior;
        BossAngerManager.OnAngryGameOver += AngerGameOverBehavior;

        pointsManager = FindObjectOfType<PointsManager>();

    }
    void OnDestroy()
    {
        TimerManagerMonoBehaviour.OnTimeFinished -= TimesOverBehavior;
        BossAngerManager.OnAngryGameOver -= AngerGameOverBehavior;
    }

    private void AngerGameOverBehavior()
    {
        if (endLevelAnimator == null) return;

        endLevelAnimator.SetBool("Star1", false);
        endLevelAnimator.SetBool("Stars2", false);
        endLevelAnimator.SetBool("Stars3", false);
        endLevelAnimator.SetTrigger("TriggerGameOver");
    }

    private void TimesOverBehavior()
    {
        if (endLevelAnimator == null) return;


        pointsManager.GetEarnedPoints();
        //Set the right flag for the correct animation
        if (pointsManager.pointsPercentage >= pointsManager.star1Threshold &&
            pointsManager.pointsPercentage < pointsManager.star2Threshold)
        {
            //1 star earned
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", false);
            endLevelAnimator.SetBool("Stars3", false);
        }
        else if (pointsManager.pointsPercentage >= pointsManager.star2Threshold && pointsManager.pointsPercentage < pointsManager.star3Threshold)
        {
            //2 stars earned
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", true);
            endLevelAnimator.SetBool("Stars3", false);
        }
        else if (pointsManager.pointsPercentage >= pointsManager.star3Threshold)
        {
            //3 stars earned
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", true);
            endLevelAnimator.SetBool("Stars3", true);
        }
        else
        {
            endLevelAnimator.SetBool("Star1", false);
            endLevelAnimator.SetBool("Stars2", false);
            endLevelAnimator.SetBool("Stars3", false);
        }
        //Trigger animation
        endLevelAnimator.SetTrigger("TriggerGameOver");
    }
}
