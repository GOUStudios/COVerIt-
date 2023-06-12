using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerWaypoint : Waypoint
{
    [Header("Despawner properties")]
    [SerializeField] float distanceToDespawn;

    void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, distanceToDespawn);
        Gizmos.color = connectingColor;
        foreach (Waypoint wp in possibleNextPoint)
        {
            if (wp != null) Gizmos.DrawLine(transform.position, wp.transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void waypointReached(NPCMovementManager npcMover)
    {
        Debug.Log($"Customer wants to leave with {npcMover.visibleWaypointsReached} WPs reached");
        if(npcMover.visibleWaypointsReached >= 1 && Vector3.Distance(transform.position, npcMover.transform.position) < distanceToDespawn)
        {
            Destroy(npcMover.gameObject);
        }
        else
        {
            base.waypointReached(npcMover);
        }
    }
}
