using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Event", menuName = "Events/Game Event", order = 1)]
public class GameEvent : ScriptableObject
{
    private readonly List<IEventListener> m_eventListeners = new List<IEventListener>();

    public void Raise()
    {
        for (int i = m_eventListeners.Count - 1; i >= 0; i--)
            m_eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(IEventListener listener)
    {
        if (!m_eventListeners.Contains(listener))
            m_eventListeners.Add(listener);
    }

    public void UnregisterListener(IEventListener listener)
    {
        if (m_eventListeners.Contains(listener))
            m_eventListeners.Remove(listener);
    }

}
