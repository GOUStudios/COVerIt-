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

    public delegate void TimePause();
    public static TimePause OnTimePause;

    public delegate void TimeResume();
    public static TimeResume OnTimeResume;

    private LevelSettingManager levelManager;

    public delegate void Wave(int wavesRemaining);
    public static Wave OnWaveStart;
    public static Wave OnWaveApproaching;
    private int currentWave;
    private bool isWaveAnnounced;
    public int waveAnticipationTime;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelSettingManager.Instance;
        currentWave = 0;
        isWaveAnnounced = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                checkWaves();

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

    private void checkWaves()
    {
        float[] wavePercentages = levelManager.waveMomentPercentages;
        if(wavePercentages != null && currentWave < wavePercentages.Length)
        {
            float nextWaveTime = wavePercentages[currentWave] * maximumTime;
            float timePassed = maximumTime - timeRemaining;
            if (!isWaveAnnounced && (timePassed > (nextWaveTime - waveAnticipationTime)))
            {
                isWaveAnnounced = true;
                Debug.Log("Wave approaching!");
                OnWaveApproaching?.Invoke(wavePercentages.Length - currentWave - 1);
            }
            if (timePassed > nextWaveTime)
            {
                currentWave++;
                Debug.Log("Wave started!");
                OnWaveStart?.Invoke(wavePercentages.Length - currentWave);
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

    public void ResumeTimer()
    {
        StartCoroutine(WaitForAnimation(7f));
    }

    public IEnumerator WaitForAnimation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isRunning = true;
        OnTimeResume?.Invoke();
    }

    public void StopTimer()
    {
        isRunning = false;
        OnTimePause?.Invoke();
    }
}
