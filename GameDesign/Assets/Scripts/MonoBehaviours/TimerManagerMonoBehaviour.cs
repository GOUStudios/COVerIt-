using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManagerMonoBehaviour : MonoBehaviour
{

    // In seconds
    private int maximumTime;
    [SerializeField] private float timeRemaining;
    private bool isRunning;

    public bool IsRunning { get { return isRunning; } }
    public int TimeRemainingSeconds { get { return (int)(timeRemaining + 1); } }
    public float TimeRemaining { get { return timeRemaining; } }

    public delegate void TimeFinish();
    public static TimeFinish OnTimeFinished;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                Debug.Log("Timer finished in Timer");
                OnTimeFinished?.Invoke();
                StopTimer();
            }

        }
    }

    public void SetTime(int seconds)
    {
        maximumTime = seconds;
    }

    public int GetTime()
    {
        return maximumTime;
    }

    public void StartTimer()
    {
        Debug.Log("Timer started in Timer");
        timeRemaining = maximumTime;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
