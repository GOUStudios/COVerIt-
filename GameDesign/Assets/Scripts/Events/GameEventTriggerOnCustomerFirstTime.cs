using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTriggerOnCustomerFirstTime : GameEventTrigger
{

    [SerializeField] CustomerTypes CustomerTypeToCheck;


    protected override bool EventTriggerCondition(Collider other)
    {
        if (other == null) return false;
        CustomerMonoBehavior C = other.GetComponent<CustomerMonoBehavior>();
        if (C != null && C.type == CustomerTypeToCheck) return true;
        return false;
    }

    protected override bool EventTriggerCondition()
    {
        return EventTriggerCondition(null);
    }
}
