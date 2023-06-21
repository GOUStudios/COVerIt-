using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTriggerOnCustomerFirstTime : GameEventTrigger
{

    [SerializeField] CustomerTypes CustomerTypeToCheck;
    [SerializeField] bool onlyForUnmakedCustomer = true;


    protected override bool EventTriggerCondition(Collider other)
    {   
        if (other == null) return false;
        if(!LevelMonobehaviour.TimeHasStarted)return false;

        CustomerMonoBehavior C = other.GetComponent<CustomerMonoBehavior>();
        if (C != null && C.type == CustomerTypeToCheck)
        {
            if (onlyForUnmakedCustomer)
            {
                if (C.wearsMask) return false;
            }
            return true;

        }
        return false;
    }

    protected override bool EventTriggerCondition()
    {
        return EventTriggerCondition(null);
    }
}
