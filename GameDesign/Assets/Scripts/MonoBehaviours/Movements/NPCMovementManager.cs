using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class NPCMovementManager : MonoBehaviour
{

    //**
    [SerializeField] public UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] public float distanceThreshHoldToReachTarget = 1.5f;
    [SerializeField] public float MaxTimeToReachTarget = 15f;

    [Header("Targets")]
    [ReadOnly][SerializeField] public Waypoint previousWayPoint;
    [SerializeField] public Waypoint targetWayPoint;
    [ReadOnly][SerializeField] public CustomerMonoBehavior targetCustomer;
    bool findingTarget = false;
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


    /// <summary>
    /// function that the first time is called tries to find a target for the antiMasker
    /// then onwards tries to reach the position where it found that target. 
    /// Every maxTimeToReachTarget recalculates where it is to find him.
    /// For this specific situation is ideal if MaxTimeToReach target is small. like every .75 s 
    /// </summary>
    /// <returns></returns>
    public bool goToTargetToUnmask()
    {
        if (!findingTarget)
        {
            TTL += Time.deltaTime;
            if (targetCustomer == null)
            {
                StartCoroutine(findATarget());
                return false;
            }
            else if (TTL > MaxTimeToReachTarget && targetCustomer != null && Vector3.Distance(transform.position, targetCustomer.transform.position) > distanceThreshHoldToReachTarget)
            {
                TTL = 0;
                agent.SetDestination(targetCustomer.transform.position);
            }
            else if (targetCustomer != null && Vector3.Distance(transform.position, targetCustomer.transform.position) <= distanceThreshHoldToReachTarget)
            {
                targetCustomer.unmaskNPC();
                targetCustomer = null;
                return true;
            }

        }

        return false;
    }
    private IEnumerator findATarget()
    {
        Debug.Log("Finding a target");
        findingTarget = true;
        GameObject[] taggedArray = GameObject.FindGameObjectsWithTag("Masked");
        int i = 0;
        int j = Random.Range(0, taggedArray.Length);
        CustomerMonoBehavior customer;
        if (taggedArray.Length > 0)
        {
            do
            {

                customer = taggedArray[j].GetComponent<CustomerMonoBehavior>();
                if (customer != null && customer.wearsMask)
                {
                    targetCustomer = customer;
                }

                j = (j + 1) % taggedArray.Length;
                i++;
                yield return null;
            }
            while (targetCustomer == null && i < taggedArray.Length);
        }

        if (i >= taggedArray.Length)
        {
            Debug.LogWarning($"No tagged masked customers were found by {name}");
        }
        else
            agent.SetDestination(targetCustomer.transform.position);

        findingTarget = false;
    }

    public void goingToWaypointTik()
    {
        if (TTL > MaxTimeToReachTarget || Vector3.Distance(transform.position, targetWayPoint.transform.position) < distanceThreshHoldToReachTarget)
        {
            // Debug.Log("Reachedway point");
            TTL = 0;
            targetWayPoint.waypointReached(this);
        }
        else TTL += Time.deltaTime;

    }


}
