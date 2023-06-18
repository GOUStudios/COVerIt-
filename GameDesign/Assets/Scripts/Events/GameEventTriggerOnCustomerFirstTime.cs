using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTriggerOnCustomerFirstTime : GameEventTrigger
{

    [SerializeField] CustomerTypes CustomerTypeToCheck;


    protected override bool EventTriggerCondition(Collider other)
    {
        CustomerMonoBehavior C= other.GetComponent<CustomerMonoBehavior>();
        if(C!=null && C.type== CustomerTypeToCheck)return true;
        return false;
    }

}
