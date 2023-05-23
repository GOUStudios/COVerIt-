using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaypoint : Waypoint
{
    [Header("Spawner properties")]
    [SerializeField] float spawnArea;

    private LevelSettingManager levelManagerSingleton;

    void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, spawnArea );
        Gizmos.color = connectingColor;
        foreach (Waypoint wp in possibleNextPoint)
        {
            if (wp != null) Gizmos.DrawLine(transform.position, wp.transform.position);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        levelManagerSingleton = LevelSettingManager.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void waypointReached(NPCMovementManager npcMover)
    {
        Debug.LogWarning("An agent had its waypoint target as a spawner. Making him go back");
        npcMover.targetWayPoint = npcMover.previousWayPoint;
        npcMover.agent.SetDestination(npcMover.targetWayPoint.transform.position);
    }

    public void Spawn(int spawnRequestWM, Dictionary<CustomerTypes, int> spawnRequestUnmasked)
    {
        foreach (var (t, toSpawn) in spawnRequestUnmasked)
        {
            for(int i = 0; i < toSpawn; i++)
            {
                spawn(t);
            }
        }
        for(int i = 0; i < spawnRequestWM; i++)
        {
            float[] maskedWeights = new float[levelManagerSingleton.MaskedWeights.Count];
            levelManagerSingleton.MaskedWeights.Values.CopyTo(maskedWeights, 0);
            int indexToSpawn = RandomUtils.GetRandomWeightedIndex(maskedWeights);
            spawn((CustomerTypes)indexToSpawn, true);
        }
    }

    private void spawn(CustomerTypes customerType, bool isMasked = false)
    {
        var prefabDict = isMasked ? levelManagerSingleton.CustomerMaskedPrefabs : levelManagerSingleton.CustomerUnmaskedPrefabs;
        Vector3 position = transform.position + new Vector3(UnityEngine.Random.Range(0f, spawnArea), 0, UnityEngine.Random.Range(0f, spawnArea));
        var newObject = Instantiate(prefabDict[customerType], position, Quaternion.identity);
        var npcMover = newObject.GetComponent<NPCMovementManager>();
        npcMover.targetWayPoint = getNextWayPoint(this);
        npcMover.previousWayPoint = this;
        npcMover.agent.SetDestination(npcMover.targetWayPoint.transform.position);

    }

}
