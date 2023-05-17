using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Public interface to tell waypoints how to act when reached.
*/
public interface WaypointInterface
{
    void waypointReached(NPCMovementManager npcMover);
}
