using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentController : MonoBehaviour
{
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private float speed = 0.3f;

    private NavMeshAgent _navMeshAgent;
    private int _curIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(waypoints[_curIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetReached())
        {
            _curIndex = (_curIndex + 1) % waypoints.Length;
            _navMeshAgent.SetDestination(waypoints[_curIndex]);
        }
    }

    private bool TargetReached()
    {
        if (!_navMeshAgent.pathPending)
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude <= 0f)
                    return true;
        return false;
    }
}
