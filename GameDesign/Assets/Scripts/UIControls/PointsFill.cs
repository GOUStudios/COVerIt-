using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PointsFill : MonoBehaviour
{
    [SerializeField] private Slider earnedPoints;
    [SerializeField] private TMP_Text textPoints;
    private float Points;

    [SerializeField] private Slider LostPoints;
    [SerializeField] private TMP_Text textLosts;
    private float Lost;

    private float currentPoints;
    private float currentLost;

    private float lerpSpeed = 1.5f;

    private PointsManager pointsManager;

  

    // Start is called before the first frame update
    void Start()
    {
        pointsManager = GetComponent<PointsManager>();
 
    }

    private void InterpolatePoints()
    {       
        earnedPoints.value = currentPoints;
        currentPoints = Mathf.Lerp(currentPoints, Points, lerpSpeed * Time.deltaTime);

        //textPoints.text = ((int)currentPoints * pointsManager.GetMaxPoints()).ToString("000");
        textPoints.text = (currentPoints * 100).ToString("000");
        Debug.Log(textPoints.text);

        if (Mathf.Approximately(currentPoints, Points))
        {
            currentPoints = Points;
        }
    }

    private void InterpolateLost()
    {
        
        currentLost = Mathf.Lerp(currentLost, Lost, lerpSpeed * Time.deltaTime);
        LostPoints.value = currentLost;

        //textLosts.text = ((int) currentLost * pointsManager.GetMaxPoints()).ToString("000");
        textLosts.text = ( currentLost * 100).ToString("000");
      
        if (Mathf.Approximately(currentLost, Lost))
        {
            currentLost = Lost;
        }
    }

    public void StartPoints()
    {
        currentPoints = 0f;
        currentLost = 0f;

        //Gets Point percentage from points manager
        //Points = pointsManager.pointsPercentage;
        //Lost = pointsManager.GetLostPoints()/pointsManager.GetMaxPoints();

        //Trial values
        Points = 0.7f;
        Lost = 0.3f;

        LostPoints.value = 1;

        InvokeRepeating("InterpolatePoints", 0f, Time.deltaTime);
        InvokeRepeating("InterpolateLost", 0f, Time.deltaTime);
    }

}
