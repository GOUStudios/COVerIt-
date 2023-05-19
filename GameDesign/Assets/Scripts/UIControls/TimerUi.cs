using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUi : MonoBehaviour
{
    private TMP_Text timerText;

    //Not necessary with timer manager 
    public float timeRemaining = 120f; // Starting timer, TBD: control it with timer manager

    private void Start()
    {
        timerText = GetComponentInChildren<TMP_Text>();
    }

    //Not necessary with the timer manager
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // Decrease timer
            DisplayTime(timeRemaining);
        }
        else
        {
            //Time is over
            Debug.Log("Tempo scaduto!");
        }
    }

    //Call from time manager update function with the current time
    public void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string timeText = string.Format("{00:00}:{01:00}", minutes, seconds);
        timerText.text = timeText;
    }
}
