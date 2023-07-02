using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnerWaypoint : Waypoint
{
    [Header("Despawner properties")]
    [SerializeField] float distanceToDespawn;
    [SerializeField] SpawnerManagerMonoBehaviour manager;
    [SerializeField] TimerManagerMonoBehaviour timerManager;

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

    public void Start()
    {
        if(manager == null)
        {
            GameObject managers = GameObject.FindGameObjectWithTag("Manager");
            manager = (SpawnerManagerMonoBehaviour) ObjectUtils.GetObjectWithInterface<SpawnerManagerMonoBehaviour>(managers);
            if(manager == null)
            {
                Debug.LogError($"Couldn't set spawner manager in {gameObject.name}");
            }
            timerManager = managers.GetComponentInChildren<TimerManagerMonoBehaviour>(); 
            if (timerManager == null)
            {
                Debug.LogError($"Couldn't set timer manager in {gameObject.name}");
            }
        }
    }


    public override void waypointReached(NPCMovementManager npcMover)
    {
        Debug.Log($"Customer wants to leave with {npcMover.visibleWaypointsReached} WPs reached");
        if(timerManager.GetTime() - timerManager.TimeRemainingSeconds >= manager.DespawnAfterSeconds)
        {
            if (npcMover.visibleWaypointsReached > 1 && Vector3.Distance(transform.position, npcMover.transform.position) < distanceToDespawn)
            {
                Destroy(npcMover.gameObject);
                return;
            }
        }
        base.waypointReached(npcMover);
    }
}
