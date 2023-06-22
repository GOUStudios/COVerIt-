using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{
    private static PointsManager instance;

    [ReadOnly][SerializeField] private float maxPoints = -1;
    public float pointsPercentage
    {
        get
        {
            if (maxPoints > 0)
                return currentPoints / maxPoints;
            else return 0;
        }
    }
    [ReadOnly][SerializeField] private int currentPoints;
    private int lostPoints;
    private int earnedPoints;
    public bool IsNegative
    {
        get
        {
            return instance.currentPoints < 0;
        }
    }
    [ReadOnly] public bool isReady;

    [Range(0f, 1f)]
    public float star1Threshold;
    [Range(0f, 1f)]
    public float star2Threshold;
    [Range(0f, 1f)]
    public float star3Threshold;

    public static PointsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PointsManager>();
                if (instance == null)
                {
                    GameObject pointsObject = new GameObject(typeof(PointsManager).Name);
                    instance = pointsObject.AddComponent<PointsManager>();
                    instance.currentPoints = 0;
                    instance.lostPoints = 0;
                    instance.earnedPoints = 0;
                    instance.isReady = false;
                }
            }

            return instance;
        }
    }


    private void Awake()
    {
        if (PointsManager.Instance != null && PointsManager.Instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            TimerManagerMonoBehaviour.OnTimeFinished += TimesOverBehavior;
            calculateMaxPoints();

        }
        StartCoroutine(IsReadyChecker());
    }

    private void TimesOverBehavior()
    {
        int numberStars = 0;
 
        if (instance.pointsPercentage >= star1Threshold &&
            instance.pointsPercentage < star2Threshold)
        {
            numberStars = 1;
        }
        else if (instance.pointsPercentage >= star2Threshold && instance.pointsPercentage < star3Threshold)
        {
            numberStars = 2;
        }
        else if (instance.pointsPercentage >= star3Threshold)
        {
            numberStars = 3;
        }
        SavePrefs(instance.GetEarnedPoints(), numberStars);
    }

    private void SavePrefs(int earnedPoints, int numberStars)
    {
        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetInt($"Points{scene.name}", earnedPoints);
        PlayerPrefs.SetInt($"Stars{scene.name}", numberStars);
    }

    public void TriggerEvent_IncrementPoints(int points)
    {
        instance.currentPoints += points;

        if (points < 0) instance.lostPoints += points;
        if (points > 0) instance.earnedPoints += points;
    }

    public void TriggerEvent_ResetPoints()
    {
        calculateMaxPoints();
        instance.currentPoints = 0;
        instance.lostPoints = 0;
        instance.earnedPoints = 0;
    }

    public int GetCurrentPoints
    {
        get
        {
            return instance.currentPoints;
        }
    }

    private void calculateMaxPoints()
    {
        StartCoroutine(gettingMaxPoints());
    }

    public float GetMaxPoints()
    {
        return instance.maxPoints;
    }

    public int GetEarnedPoints()
    {
        return instance.earnedPoints;
    }

    public int GetLostPoints()
    {
        return instance.lostPoints;
    }

    IEnumerator IsReadyChecker()
    {
        
        yield return new WaitWhile(()=> maxPoints<0);
        isReady = true;
    }
    IEnumerator gettingMaxPoints()
    {
        Debug.Log("Waiting for LevelSetting to be ready");
        yield return new WaitWhile(() => !LevelSettingManager.Instance.instancesAreSet);
        Debug.Log("getting max points");
        maxPoints = LevelSettingManager.Instance.getInitialMaxPoints();

    }


}