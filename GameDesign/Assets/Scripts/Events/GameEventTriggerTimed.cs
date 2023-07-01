using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTriggerTimed : GameEventTrigger
{
    [SerializeField] float WaitTimeAfterLevelStarts = 0f;
    float passedTime = 0f;
    protected override bool EventTriggerCondition(Collider other) => passedTime >= WaitTimeAfterLevelStarts;

    void Update()
    {
        if (TimerManagerMonoBehaviour.IsRunning)
        {
            passedTime += Time.deltaTime;
            TriggerEventViaCode();
        }
    }

}
