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

    [SerializeField] private TMP_Text textTotal;
    private float Total;

    private float currentPoints;
    private float currentLost;
    private float currentTotal;

    private float lerpSpeed = 3f;

    private PointsManager pointsManager;



    // Start is called before the first frame update
    void Start()
    {
        pointsManager = PointsManager.Instance;
    }

    private void InterpolatePoints()
    {
        currentPoints = Mathf.Lerp(currentPoints, Points, lerpSpeed * Time.deltaTime);

        //textPoints.text = (currentPoints * pointsManager.GetMaxPoints()).ToString("000");
        textPoints.text = (currentPoints).ToString("F0");

        if (Mathf.Approximately(currentPoints, Points))
        {
            currentPoints = Points;
        }

        earnedPoints.value = currentPoints / pointsManager.GetMaxPoints();
    }

    private void InterpolateLost()
    {
        currentLost = Mathf.Lerp(currentLost, Lost, lerpSpeed * Time.deltaTime);

        //textLosts.text = (currentLost ).ToString("000");
        textLosts.text = (currentLost).ToString("F0");

        if (Mathf.Approximately(currentLost, Lost))
        {
            currentLost = Lost;
        }
        LostPoints.value = currentLost * -1 / pointsManager.GetMaxPoints();
    }

    private void InterpolateTotal()
    {
        currentTotal = Mathf.Lerp(currentTotal, Total, lerpSpeed * Time.deltaTime);

        //textLosts.text = (currentLost * pointsManager.GetMaxPoints()).ToString("000");
        textTotal.text = (currentTotal).ToString("F0");

        if (Mathf.Approximately(currentTotal, Total))
        {
            currentTotal = Total;
        }
    }

    public void StartPoints()
    {
        currentPoints = 0f;
        currentLost = 0f;
        currentTotal = 0f;

        //Gets Point percentage from points manager
        Points = pointsManager.GetEarnedPoints();
        Lost = pointsManager.GetLostPoints();
        Total = pointsManager.GetCurrentPoints;

        //Trial values
        //Points = 0.7f;
        //Lost = 0.3f;
        //Total = 70f;

        LostPoints.value = 1;

        InvokeRepeating("InterpolatePoints", 0f, Time.deltaTime);
        InvokeRepeating("InterpolateLost", 0f, Time.deltaTime);
        InvokeRepeating("InterpolateTotal", 0f, Time.deltaTime);
    }

}
