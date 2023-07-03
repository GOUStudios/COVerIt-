using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUi : MonoBehaviour
{
    private TMP_Text timerText;
    [SerializeField] private TimerManagerMonoBehaviour timeManager;

     
    private float timeRemaining; // Starting timer


    private void Start()
    {
        if (timeManager != null)
        {
            timeRemaining = timeManager.GetTime();
        }
        timerText = GetComponentInChildren<TMP_Text>();
    }

    //Not necessary with the timer manager
    void Update()
    {
        if (timeManager == null) return;
            timeRemaining = timeManager.TimeRemaining; // Decrease timer done in the manager
            DisplayTime(timeRemaining);
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
