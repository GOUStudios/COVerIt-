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
        if (!m_hasGameEvent) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (TimerManagerMonoBehaviour.IsRunning)
        {
            passedTime += Time.deltaTime;
            if (EventTriggerCondition())
            {
                m_event.Raise();
                m_hasGameEventBeenTriggered = true;
                if (m_TriggerOnce) enabled = false;
            }
        }
    }
    
}
