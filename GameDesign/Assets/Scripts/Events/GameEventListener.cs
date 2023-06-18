using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour, IEventListener
{
    [Tooltip("Event to register with")]
    [SerializeField] private GameEvent @event;


    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField] private UnityEvent response;

    void OnEnable()
    {
        if (@event != null) @event.RegisterListener(this);
    }

    void OnDisable()
    {
        if (@event != null) @event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
