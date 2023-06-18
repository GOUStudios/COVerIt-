using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event", menuName = "Events/Timed event", order = 1)]
public class TimedEvent : GameEvent
{
    // Start is called before the first frame update
    public float Duration = -1f;
}