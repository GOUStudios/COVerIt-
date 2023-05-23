using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettingManager
{
    #region attributes
    private static LevelSettingManager instance_;


    public int CustomersToBeSpawnedWM { get; private set; }
    public Dictionary<CustomerTypes, int> CustomersWithOutMask;
    public Dictionary<CustomerTypes, GameObject> CustomerMaskedPrefabs;
    public Dictionary<CustomerTypes, GameObject> CustomerUnmaskedPrefabs;

    /*
    *public variable that tells if pulling of costumers is being done
    */
    public static bool isSpawning { get; private set; }
    public static bool isReadyToSpawn { get; private set; }

    private int leftSpawnedWM;
    public Dictionary<CustomerTypes, int> leftCustomersWithOutMask;
    public Dictionary<CustomerTypes, float> MaskedWeights { get; private set; }

    public static LevelSettingManager Instance
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = new LevelSettingManager();
            }
            return instance_;
        }
    }

    #endregion

    #region Events

    public delegate void OnSpawn(int spawnerID, int masked, Dictionary<CustomerTypes, int> unmasked);

    /*
    * DO NOT Directly invoke spawn, it should do a pull request of the level manager
    * then the level manager will trigger the event and tell which spawner to spawn.
    */
    public event OnSpawn Spawn;



    public (int, Dictionary<CustomerTypes, int>) RequestSpawn(int spawns, Dictionary<CustomerTypes, int> UnmaskedSpawns)
    {
        //TODO Define where we want to decide the amount of people to be spawned, in the spawner or the level manager
        //if we want it to be in the spawners, we should add the amount to be requested.
        isSpawning = true;
        // Dictionary<CustomerTypes, int> temp = { 0, 0 };
        var pullResponse = Pull(spawns, UnmaskedSpawns);
        isSpawning = false;
        // Spawn?.Invoke(SpawnerID, 0, temp);
        return pullResponse;

    }


    #endregion

    public LevelSettingManager()
    {
        CustomersWithOutMask = new Dictionary<CustomerTypes, int>();
        CustomersToBeSpawnedWM = 0;
        isSpawning = false;
        isReadyToSpawn = false;

    }

    internal void SetPrefabs(Dictionary<CustomerTypes, GameObject> unmaskedPrefabsDictionary, Dictionary<CustomerTypes, GameObject> maskedPrefabsDictionary)
    {
        isReadyToSpawn = false;
        CustomerMaskedPrefabs = maskedPrefabsDictionary;
        CustomerUnmaskedPrefabs = unmaskedPrefabsDictionary;
        if (CustomersWithOutMask != null && leftCustomersWithOutMask != null) isReadyToSpawn = true;
    }

    /*
    * To be called by the Level monobehaviour to set the level spawns
    *  !! Try not to call it outside of it !!
    */
    public void SetTotalSpawns(int spawns, Dictionary<CustomerTypes, float> maskedWeights, Dictionary<CustomerTypes, int> UnmaskedSpawns)
    {
        isReadyToSpawn = false;
        if (spawns > 0)
        {
            CustomersToBeSpawnedWM = leftSpawnedWM = spawns;
        }

        MaskedWeights = maskedWeights;
        CustomersWithOutMask = UnmaskedSpawns;
        leftCustomersWithOutMask = new Dictionary<CustomerTypes, int>(CustomersWithOutMask);
        if(CustomerMaskedPrefabs != null && CustomerUnmaskedPrefabs != null) isReadyToSpawn = true;
        // else
        // {
        //     Debug.LogError("The amount of types of customers to spawn was higher than the maximum amount possible\n");
        // }

    }

    /*
    *This function should be called before spawning to tell the service that it is possible to spawn.
    *if its not possible for now it gives an error
    */
    private (int, Dictionary<CustomerTypes, int>) Pull(int spawns, Dictionary<CustomerTypes, int> UnmaskedSpawns)
    {
        if (isReadyToSpawn)
        {
            Debug.Log($"Received request to spawn {spawns}, {UnmaskedSpawns}");
            int actualSpawns = leftSpawnedWM > 0 ? Math.Min(leftSpawnedWM, spawns) : 0;
            if (leftSpawnedWM > 0) leftSpawnedWM = Math.Max(0, leftSpawnedWM - spawns);

            Dictionary<CustomerTypes, int> actualSpawnsUnmasked = new Dictionary<CustomerTypes, int>();
            foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes)))
            {
                if(UnmaskedSpawns.ContainsKey(t))
                {
                    int leftForCustomerType = leftCustomersWithOutMask[t];
                    int actualSpawn = leftCustomersWithOutMask[t] > 0 ? Math.Min(leftForCustomerType, UnmaskedSpawns[t]) : 0;
                    actualSpawnsUnmasked.Add(t, actualSpawn);
                    if (leftSpawnedWM > 0) leftCustomersWithOutMask[t] = Math.Max(0, leftForCustomerType - UnmaskedSpawns[t]);
                }
            }
            return (actualSpawns, actualSpawnsUnmasked);
        }
        else {
            var dict = new Dictionary<CustomerTypes, int>();
            foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes))) { dict.Add(t, 0); }
            return (0, dict);
        }
    }

}
