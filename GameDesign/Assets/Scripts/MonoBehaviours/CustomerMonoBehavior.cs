using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] public int id;
    [SerializeField] public float baseSpeed;
    SerializeField] public float currentSpeed;
    [SerializeField] public int clickAmount = 0;
    [SerializeField] public int clickTime;
    // [SerializeField] public Action action; Activty that customer is going to perform in each waypoint 
    [SerializeField] public bool wearsMask;
    [SerializeField] public NPCMovementManager moveManager;
    [SerializeField] public int pointValue;
    [SerializeField] public bool isFrozen = false;
    [SerializeField] public int frozenTimeCount = 0;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    // Start is called before the first frame update

    public void Click(ClickType clickType)
    {
        if(clickType == ClickType.LEFT_CLICK)
        {
            clickAmount++;
            if (!wearsMask)
            {
                wearsMask = true;
                PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
            }
            else
            {
                PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
            }
        }

        if(clickType == clickType.RIGHT_CLICK)
        {
            if(!isFrozen)
            {
                isFrozen = true;
                InvokeRepeating("unFreeze", 1.0f, 1.0f);
                currentSpeed = 0;
            }
        }
    }

    private void UnFreeze()
    {
        if(frozenTimeCount == 3)
        {
            isFrozen = false;
            currentSpeed = baseSpeed;
            frozenTimeCount = 0;
        }else
        {
            frozenTimeCount++;
        }
    }

    bool hasMask()
    {
        return true;
    }
}
