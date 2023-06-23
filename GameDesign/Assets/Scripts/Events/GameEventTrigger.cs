using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventTrigger : MonoBehaviour
{
    [SerializeField] private bool m_TriggerOnce;
    private bool m_hasGameEventBeenTriggered;
    [SerializeField] private bool m_useEnterinOrExiting = false;
    [SerializeField] private GameEvent m_event;
    private bool m_hasGameEvent = true;

    protected abstract bool EventTriggerCondition(Collider other);
    protected virtual bool EventTriggerCondition() {
        return EventTriggerCondition(null);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!m_useEnterinOrExiting) return;
        if (!m_hasGameEvent) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (EventTriggerCondition(other))
        {
            m_event.Raise();
            m_hasGameEventBeenTriggered = true;
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
        }

    }


    public void OnTriggerExit(Collider other)
    {
        if (m_useEnterinOrExiting) return;
        if (!m_hasGameEvent) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (EventTriggerCondition(other))
        {
            m_event.Raise();
            m_hasGameEventBeenTriggered = true;
        }


    }
}