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


    void Start()
    {
        if (agent == null)
        {
            agent = (UnityEngine.AI.NavMeshAgent)GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent == null) Debug.LogError("Could not find NavMeshAgent");
        }

        agent.SetDestination(previousWayPoint.transform.position);

    }


    void Update()
    {
        if (Vector3.Distance(transform.position, targetWayPoint.transform.position) < distanceThreshHoldToReachWP)
        {
            Debug.Log("Reachedway point");
            targetWayPoint.waypointReached(this);
        }

    }


}
