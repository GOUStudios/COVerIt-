
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour, WaypointInterface
{

    [SerializeField] Waypoint[] possibleNextPoint;
    [SerializeField] int maxTriesForRandomPick = 40;

    [SerializeField] bool pickRandomly = true;
    [SerializeField] int nextDefaultDestination = 0;
    [SerializeField] bool changableNextDefaultDestination = true;
    [SerializeField] bool setAmount = false;
    [SerializeField] int defaultIndexChanger = 1;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, 1f);
        Gizmos.color = Color.red;

        foreach (Waypoint wp in possibleNextPoint)
        {
            Gizmos.DrawLine(this.transform.position, wp.transform.position);
        }

    }


    Waypoint getNextWayPoint(Waypoint previousWP)
    {
        int i = nextDefaultDestination;
        int counter = 0;
        bool selected = false;
        if (possibleNextPoint.Length < 1)
        {
            Debug.LogError("There are no possible destinations");
            return null;
        }
        else
        {
            if (pickRandomly)
            {
                do
                {
                    i = Random.Range(0, possibleNextPoint.Length);
                    if (possibleNextPoint[i] != previousWP) selected = true;
                } while (!selected && maxTriesForRandomPick > counter++);
            }

            if (!pickRandomly || counter >= maxTriesForRandomPick)
            {
                i = nextDefaultDestination;
                if (changableNextDefaultDestination)
                {
                    if (setAmount)
                        nextDefaultDestination = (defaultIndexChanger + nextDefaultDestination) % possibleNextPoint.Length;
                    else
                        nextDefaultDestination = (Random.Range(0, possibleNextPoint.Length) + nextDefaultDestination) % possibleNextPoint.Length;

                }

            }
        }
        return possibleNextPoint[i];
    }


    /* Function on Each type of wayPoint that determines what to do when a waypoint is reached
    by default it just gets next wayPoint and asks to go to it.
    despawners can have different logic in here.
    or certain ' event' ones as well. 
    also if we want to change parameters like the distance the agent has to check to see if it reached the point can be set here.*/

    public void waypointReached(NPCMovementManager npcMover)
    {
        npcMover.targetWayPoint = getNextWayPoint(npcMover.previousWayPoint);
        npcMover.previousWayPoint = this;
        npcMover.agent.SetDestination(npcMover.targetWayPoint.transform.position);
    }
}
