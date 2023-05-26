using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NPCMovementManager : MonoBehaviour
{
    [SerializeField] public UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] public Waypoint previousWayPoint;
    [SerializeField] public Waypoint targetWayPoint;
    [SerializeField] public float distanceThreshHoldToReachWP = 1.5f;
    [SerializeField] public float MaxTimeToReachWaypoint = 15f;

    private float TTL = 0f;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent == null) Debug.LogError("Could not find NavMeshAgent");
        }
        if (targetWayPoint == null)
        {
            Debug.LogError($"{this.name}: Missing initial variable targetWaypoint.");
        }

        agent.SetDestination(targetWayPoint.transform.position);

    }


    void Update()
    {
        if (TTL > MaxTimeToReachWaypoint || Vector3.Distance(transform.position, targetWayPoint.transform.position) < distanceThreshHoldToReachWP)
        {
            // Debug.Log("Reachedway point");
            TTL = 0;
            targetWayPoint.waypointReached(this);
        }
        else TTL += Time.deltaTime;


    }


}
