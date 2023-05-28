using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour
{
    private static PointsManager instance;

    private int currentPoints;
    public bool IsNegative { get
        {
            return instance.currentPoints < 0;
        }
    }

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
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void TriggerEvent_IncrementPoints(int points)
    {
        instance.currentPoints += points;
        Debug.Log($"Increasing points {points}, now is {instance.currentPoints}");
    }

    public void TriggerEvent_ResetPoints()
    {
        instance.currentPoints = 0;
    }

    public int GetCurrentPoints()
    {
        return instance.currentPoints;
    }

}