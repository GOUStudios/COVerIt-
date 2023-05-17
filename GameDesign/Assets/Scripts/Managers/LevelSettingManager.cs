using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettingManager
{
    #region attributes
    private static LevelSettingManager instance_;


    public int CustomersToBeSpawnedWM { get; private set; }
    public int[] CustomersWithOutMask { get; private set; }

    /*
    *public variable that tells if pulling of costumers is being done
    */
    public static bool isSpawning { get; private set; }




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

    public delegate void OnSpawn(int spawnerID, int masked, int[] unmasked);

    /*
    * DO NOT Directly invoke spawn, it should do a pull request of the level manager
    * then the level manager will trigger the event and tell which spawner to spawn.
    */
    public event OnSpawn Spawn;



    public void requestSpawn(int SpawnerID)
    {
        //TODO Define where we want to decide the amount of people to be spawned, in the spawner or the level manager
        //if we want it to be in the spawners, we should add the amount to be requested.
        isSpawning = true;
        int[] temp = { 0, 0 };
        pull(0, temp);
        isSpawning = false;
        Spawn?.Invoke(SpawnerID, 0, temp);
        
    }


    #endregion

    public LevelSettingManager()
    {
        CustomersWithOutMask = new int[Enum.GetValues(typeof(CustomerTypes)).Length];
        CustomersToBeSpawnedWM = 0;
        isSpawning = false;

    }

    /*
    * To be called by the Level monobehaviour to set the level spawns
    *  !! Try not to call it outside of it !!
    */
    public void setTotalSpawns(int spawns, int[] UnmaskedSpawns)
    {
        if (spawns > 0)
        {
            CustomersToBeSpawnedWM = spawns;
        }
        if (UnmaskedSpawns.Length <= CustomersWithOutMask.Length)
        {
            int i;
            for (i = 0; i < UnmaskedSpawns.Length; i++)
            {
                if (UnmaskedSpawns[i] > 0) CustomersWithOutMask[i] = UnmaskedSpawns[i];
                else CustomersWithOutMask[i] = 0;
            }
            for (; i < CustomersWithOutMask.Length; i++)
            {
                CustomersWithOutMask[i] = 0;
            }
        }
        else { Debug.LogError("The amount of types of customers to spawn was higher than the maximum amount possible\n"); }

    }

    /*
    *This function should be called before spawning to tell the service that it is possible to spawn.
    *if its not possible for now it gives an error
    */
    public void pull(int spawns, int[] UnmaskedSpawns)
    {
        #region errorChecking
        /*
        * It is asked that the customers be >=0, && <remaining customers of that type
        * the <0 could be change to spawn 0 customers if need be. and the over amount could be made to spawn the max amount
        * to be decided how to implement
        */
        isSpawning = true;
        if (spawns > CustomersToBeSpawnedWM)
        {
            //FOR now it just gives error
            Debug.LogError("Trying to spawn too many costumers with out mask:\n Tried: " + spawns
            + "\n Max: " + CustomersToBeSpawnedWM);
            return;
        }
        if (UnmaskedSpawns.Length > CustomersWithOutMask.Length)
        {
            Debug.LogError("The amount of types of customers to spawn was higher than the maximum amount possible\n");
            return;
        }
        for (int i = 0; i < UnmaskedSpawns.Length; i++)
        {

            if (UnmaskedSpawns[i] < 0 || UnmaskedSpawns[i] > CustomersWithOutMask[i])
            {
                Debug.LogError("Error trying to spawn costumers:" + Enum.GetNames(typeof(CustomerTypes))[i] + ":\n Tried: " + UnmaskedSpawns[i] + "\n Max: " + CustomersToBeSpawnedWM);
                return;
            }
        }
        #endregion


    }

}