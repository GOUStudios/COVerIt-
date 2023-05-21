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
        if(Vector3.Distance(transform.position, npcMover.transform.position) < distanceToDespawn)
        {
            Destroy(npcMover.gameObject);
        }
    }
}
