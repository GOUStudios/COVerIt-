using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesOutUI : MonoBehaviour
{
    private PointsManager pointsManager;

    [SerializeField] private Animator endLevelAnimator;

    public float star1Threshold;
    public float star2Threshold;
    public float star3Threshold;

    // Start is called before the first frame update
    void Start()
    {
        TimerManagerMonoBehaviour.OnTimeFinished += TimesOverBehavior;

        pointsManager = GetComponent<PointsManager>();
     
    }


    private void TimesOverBehavior()
    {
        float PointsEarned = (float)pointsManager.GetCurrentPoints();
        float PointsLost = (float)pointsManager.GetLostPoints();

        
        //Set the right flag for the correct animation
        if(pointsManager.pointsPercentage >= star1Threshold && pointsManager.pointsPercentage <= star2Threshold)
        {
            //1 star earned
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", false);
            endLevelAnimator.SetBool("Stars3", false);
        }
        else if(pointsManager.pointsPercentage >= star2Threshold && pointsManager.pointsPercentage <= star3Threshold)
        {
            //2 stars earned
            endLevelAnimator.SetBool("Star1", true);
            endLevelAnimator.SetBool("Stars2", true);
            endLevelAnimator.SetBool("Stars3", false);
        }
        else if(pointsManager.pointsPercentage >= star3Threshold)
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
