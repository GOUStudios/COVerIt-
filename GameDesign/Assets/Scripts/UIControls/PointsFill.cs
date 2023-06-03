using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PointsFill : MonoBehaviour
{
    [SerializeField] private Slider earnedPoints;
    private float Points;

    [SerializeField] private Slider LostPoints;
    private float Lost;

    private float currentPoints;
    private float currentLost;

    private float lerpSpeed;

    private PointsManager pointsManager;

  

    // Start is called before the first frame update
    void Start()
    {
        pointsManager = GetComponent<PointsManager>();

        
        
    }

    private void InterpolatePoints()
    {
        // Incrementa gradualmente il valore corrente verso il valore target
        earnedPoints.value = currentPoints;
        currentPoints = Mathf.Lerp(currentPoints, Points, lerpSpeed * Time.deltaTime);
        
        
        Debug.Log("Curretn points: " + currentPoints);

        // Controlla se si è raggiunto il valore target
        if (Mathf.Approximately(currentPoints, Points))
        {
            // Interpolazione completata
            Debug.Log("Interpolazione completata!");
        }
    }

    private void InterpolateLost()
    {
        // Incrementa gradualmente il valore corrente verso il valore target
        LostPoints.value = currentLost;
        currentLost = Mathf.Lerp(currentLost, Lost, lerpSpeed * Time.deltaTime);


        Debug.Log("Curretn points: " + currentLost);
        Debug.Log(" points: " + Points);
        // Controlla se si è raggiunto il valore target
        if (Mathf.Approximately(currentLost, Lost))
        {
            // Interpolazione completata
            Debug.Log("Interpolazione completata!");
        }
    }

    public void StartPoints()
    {
        // Resetta il valore corrente all'inizio dell'interpolazione
        currentPoints = 0f;
        currentLost = 0f;

        //Points = pointsManager.pointsPercentage;
        //
        //Lost = pointsManager.GetLostPoints()/pointsManager.GetMaxPoints();

        Points = 0.7f;
        
        Lost = 0.3f;

        // Chiama InterpolateValue ogni frame finché il valore target non viene raggiunto
        InvokeRepeating("InterpolatePoints", 0f, Time.deltaTime);
        InvokeRepeating("InterpolateLost", 0f, Time.deltaTime);
    }

}
