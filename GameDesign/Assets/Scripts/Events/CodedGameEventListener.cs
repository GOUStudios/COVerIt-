using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CodedGameEventListener : IEventListener
{
    [SerializeField] private GameEvent @event;
    private Action m_onResponse;

    public void OnEventRaised()
    {
        m_onResponse?.Invoke();
    }

    void OnEnable(Action response)
    {
        if(@event!= null)@event.RegisterListener(this);
        m_onResponse = response;
    }
    void OnDisable()
    {
        if(@event!=null)@event.UnregisterListener(this);
        m_onResponse = null;
    }
}
