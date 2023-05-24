using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] public int id;
    [SerializeField] public float baseSpeed;
    [SerializeField] public float currentSpeed;
    [SerializeField] public int clickAmount = 0;
    [SerializeField] public int clickTime;
    [SerializeField] public bool wearsMask;
    [SerializeField] public NPCMovementManager moveManager;
    [SerializeField] public int pointValue;
    [SerializeField] public bool isFrozen = false;
    [SerializeField] public int frozenTimeCount = 0;

    [SerializeField] public TaserManager taserManager = TaserManager.Instance;

    void Start()
    {
        InvokeRepeating("UnFreeze", 1.0f, 1.0f);
        currentSpeed = baseSpeed;
    }

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
        if(clickType == ClickType.RIGHT_CLICK)
        {
            if (taserManager.taserBattery >= taserManager.taserCost)
            {
                taserManager.taserBattery = taserManager.taserBattery - taserManager.taserCost;
                if(!isFrozen)
                {
                    isFrozen = true;
                    currentSpeed = 0;
                    frozenTimeCount = 0; // Restart counter
                }
            }
        }
    }

    private void UnFreeze()
    {
        if (isFrozen){
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
    }

    bool hasMask()
    {
        return true;
    }
}
