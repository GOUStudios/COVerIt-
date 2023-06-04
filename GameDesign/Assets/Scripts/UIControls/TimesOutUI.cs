using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesOutUI : MonoBehaviour
{
    private PointsManager pointsManager;

    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    [SerializeField] private Image levelFailed;
    [SerializeField] private Image levelCompleted;

    public float star1Threshold;
    public float star2Threshold;
    public float star3Threshold;

    // Start is called before the first frame update
    void Start()
    {
        TimerManagerMonoBehaviour.OnTimeFinished += TimesOverBehavior;

        pointsManager = GetComponent<PointsManager>();

        levelCompleted.enabled = true;
        levelFailed.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void TimesOverBehavior()
    {
        float PointsEarned = (float)pointsManager.GetCurrentPoints();
        float PointsLost = (float)pointsManager.GetLostPoints();

        if(pointsManager.pointsPercentage >= star1Threshold && pointsManager.pointsPercentage <= star2Threshold)
        {
            //1 star earned
            star1.enabled = true;
        }
        else if(pointsManager.pointsPercentage >= star2Threshold && pointsManager.pointsPercentage <= star3Threshold)
        {
            //2 stars earned
            star1.enabled = true;
            star2.enabled = true;

        }
        else if(pointsManager.pointsPercentage >= star3Threshold)
        {
            //3 stars earned
            star1.enabled = true;
            star2.enabled = true;
            star3.enabled = true;
        }
        else
        {
            
        }

        //

               
    }
}
