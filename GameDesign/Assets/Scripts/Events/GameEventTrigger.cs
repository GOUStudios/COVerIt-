using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventTrigger : MonoBehaviour
{
    [SerializeField] protected bool m_TriggerOnce;
    protected bool m_hasGameEventBeenTriggered;
    [SerializeField] protected bool m_useEnterinOrExitingHitBox = false;
    [SerializeField] protected GameEvent m_event;
    protected bool m_hasGameEvent = true;

    protected abstract bool EventTriggerCondition(Collider other);
    protected virtual bool EventTriggerCondition()
    {
        return EventTriggerCondition(null);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!m_useEnterinOrExitingHitBox) return;
        if (!m_hasGameEvent) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (EventTriggerCondition(other))
        {
            m_event.Raise();
            m_hasGameEventBeenTriggered = true;
            if (m_TriggerOnce) enabled = false;
        }

    }

    public void TriggerEventViaCode()
    {

        if (!m_hasGameEvent) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (EventTriggerCondition())
        {
            m_event.Raise();
            m_hasGameEventBeenTriggered = true;
            if (m_TriggerOnce) enabled = false;
        }

    }


    public void OnTriggerExit(Collider other)
    {
        if (m_useEnterinOrExitingHitBox) return;
        if (!m_hasGameEvent) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (EventTriggerCondition(other))
        {
            m_event.Raise();
            m_hasGameEventBeenTriggered = true;
            if (m_TriggerOnce) enabled = false;
        }


    }
}
