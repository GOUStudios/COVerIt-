using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesOutUI : MonoBehaviour
{
    private PointsManager pointsManager;

    [SerializeField] private Animator endLevelAnimator;

    [Range(0f,1f)]
    public float star1Threshold;
    [Range(0f, 1f)]
    public float star2Threshold;
    [Range(0f, 1f)]
    public float star3Threshold;

    // Start is called before the first frame update
    void Start()
    {
        TimerManagerMonoBehaviour.OnTimeFinished += TimesOverBehavior;

        pointsManager = FindObjectOfType<PointsManager>();

    }

    private void TimesOverBehavior()
    {
        if (endLevelAnimator == null) return;
        

        pointsManager.GetEarnedPoints();
        int numberStars = 0;
        //Set the right flag for the correct animation
        if (pointsManager.pointsPercentage >= star1Threshold && 
            pointsManager.pointsPercentage <= star2Threshold)
        {
            //1 star earned
            numberStars = 1;
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", false);
            endLevelAnimator.SetBool("Stars3", false);
        }
        else if (pointsManager.pointsPercentage >= star2Threshold && pointsManager.pointsPercentage <= star3Threshold)
        {
            //2 stars earned
            numberStars = 2;
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", true);
            endLevelAnimator.SetBool("Stars3", false);
        }
        else if (pointsManager.pointsPercentage >= star3Threshold)
        {
            //3 stars earned
            numberStars = 3;
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

        SavePrefs(pointsManager.GetEarnedPoints(), numberStars);
        //Trigger animation
        endLevelAnimator.SetTrigger("TriggerGameOver");
    }

    private void SavePrefs(int earnedPoints, int numberStars)
    {
        PlayerPrefs.SetInt("PointsLvl1", earnedPoints);
        PlayerPrefs.SetInt("StarsLvl1", numberStars);
    }
}
