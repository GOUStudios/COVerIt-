
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour, WaypointInterface
{
    [Header("Same floor destinations:")]
    [SerializeField] Waypoint[] possibleNextPoint;

    [Header("Picker settings:")]
    [SerializeField] int maxTriesForRandomPick = 40;

    [SerializeField] bool pickRandomly = true;
    [SerializeField] int nextDefaultDestination = 0;
    [SerializeField] bool changableNextDefaultDestination = true;
    [SerializeField] bool setAmount = false;
    [SerializeField] int defaultIndexChanger = 1;

    [Header("Gizmos parameters:")]
    [SerializeField] Color connectigColor = Color.red;
    [SerializeField] float sphereRadius = 1f;
    [SerializeField] Color sphereColor = Color.blue;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, sphereRadius);
        Gizmos.color = connectigColor;

        foreach (Waypoint wp in possibleNextPoint)
        {
            if (wp != null) Gizmos.DrawLine(this.transform.position, wp.transform.position);
        }

    }


    protected Waypoint getNextWayPoint(Waypoint previousWP)
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
                    if (possibleNextPoint[i] != previousWP && possibleNextPoint[i] != null) selected = true;
                } while (!selected && maxTriesForRandomPick > counter++);
            }

            if (!pickRandomly || counter >= maxTriesForRandomPick)
            {
                i = nextDefaultDestination;
                int j = i;
                if (possibleNextPoint[j] == null)
                {
                    Debug.LogError($"The waypoint(#{j}) in {this.name} was null");

                    j = 0;
                }
                while (possibleNextPoint[j] == null && j < possibleNextPoint.Length)
                {
                    j++;
                }
                if (j == possibleNextPoint.Length)
                {
                    Debug.LogError($"there were no valid way points in {this.name}");
                }
                if (changableNextDefaultDestination)
                {
                    if (setAmount)
                    {
                        nextDefaultDestination = (defaultIndexChanger + nextDefaultDestination) % possibleNextPoint.Length;
                    }

                    else
                    {
                        nextDefaultDestination = (Random.Range(0, possibleNextPoint.Length) + nextDefaultDestination) % possibleNextPoint.Length;
                    }


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

    public virtual void waypointReached(NPCMovementManager npcMover)
    {
        npcMover.targetWayPoint = getNextWayPoint(npcMover.previousWayPoint);
        npcMover.previousWayPoint = this;
        npcMover.agent.SetDestination(npcMover.targetWayPoint.transform.position);
    }
}
