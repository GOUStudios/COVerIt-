using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventTrigger : MonoBehaviour
{
    [SerializeField] protected bool m_TriggerOnce;
    [SerializeField] protected bool m_TriggerOncePerGameFile = false;
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
        TriggerEventViaCode(other);

    }
    public void TriggerEventViaCode(Collider other)
    {
        if (!m_hasGameEvent) return;
        if (m_TriggerOncePerGameFile && PlayerPrefs.GetInt(m_event.name, 0) != 0) return;
        if (m_TriggerOnce && m_hasGameEventBeenTriggered) return;

        if (EventTriggerCondition(other))
        {
            m_event.Raise();
            m_hasGameEventBeenTriggered = true;
            if (m_TriggerOncePerGameFile) PlayerPrefs.SetInt(m_event.name, 1);
            if (m_TriggerOnce) enabled = false;

        }

    }
    public void TriggerEventViaCode()
    {
        TriggerEventViaCode(null);
    }


    public void OnTriggerExit(Collider other)
    {
        if (m_useEnterinOrExitingHitBox) return;
        TriggerEventViaCode(other);
    }
}
