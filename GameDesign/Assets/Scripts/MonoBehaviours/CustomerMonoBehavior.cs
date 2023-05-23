using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] private int id;
    [SerializeField] private float speed;
    [SerializeField] private int clickAmount = 0;
    [SerializeField] private int clickTime;
   // [SerializeField] private Action action; Activty that customer is going to perform in each waypoint 
    [SerializeField] private bool wearsMask;
    [SerializeField] private NPCMovementManager moveManager;
    [SerializeField] private int pointValue;
    [SerializeField] private bool isFrozen;

    // Start is called before the first frame update
   
    public void Click(ClickType clickType)
    {
        Debug.Log($"Click on customer");
        if (clickType == ClickType.LEFT_CLICK)
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
    }

    bool hasMask()
    {
        return true;
    }
}
