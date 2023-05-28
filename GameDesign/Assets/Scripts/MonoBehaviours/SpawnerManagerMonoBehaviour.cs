using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerMonoBehaviour : MonoBehaviour
{

    [SerializeField] LevelSettingManager leverManagerSingleton;
    [SerializeField] TimerManagerMonoBehaviour timerManager;

    [EnumNamedArray(typeof(CustomerTypes))]
    [SerializeField] AnimationCurve[] spawnRatesArray;

    [SerializeField] AnimationCurve maskedSpawnRate;

    [SerializeField] SpawnerWaypoint[] levelSpawners;

    [Range(0.0f, 1.0f)]
    [SerializeField] float pullRate;

    private float currentTime = 0f;
    private float currentPullRate = 0f;

    private Dictionary<CustomerTypes, AnimationCurve> spawnRates = new Dictionary<CustomerTypes, AnimationCurve>();
    // Start is called before the first frame update
    void Start()
    {
        leverManagerSingleton = LevelSettingManager.Instance;
        if (timerManager == null)
        {
            timerManager = GetComponent<TimerManagerMonoBehaviour>();
            if (timerManager == null)
            {
                Debug.LogError("Couldn't find timer manager");
            }
        }
        foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes)))
        {
            spawnRates.Add(t, spawnRatesArray[(int)t]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        float timePercentage = currentTime / timerManager.GetTime();
        if (currentTime >= currentPullRate && timePercentage < 1f)
        {
            float p = UnityEngine.Random.Range(0f, 1.01f);

            Dictionary<CustomerTypes, int>[] toSpawnUnmasked = new Dictionary<CustomerTypes, int>[levelSpawners.Length];
            for (int i = 0; i < toSpawnUnmasked.Length; i++) toSpawnUnmasked[i] = new Dictionary<CustomerTypes, int>();
            foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes)))
            {
                if (spawnRates[t].Evaluate(timePercentage) >= p)
                {
                    toSpawnUnmasked[UnityEngine.Random.Range(0, levelSpawners.Length)].Add(t, 1);
                }
            }
            for (int i = 0; i < toSpawnUnmasked.Length; i++)
            {
                Dictionary<CustomerTypes, int> toSpawn = toSpawnUnmasked[i];

                p = UnityEngine.Random.Range(0f, 1.01f);
                int spawnMasked = maskedSpawnRate.Evaluate(timePercentage) >= p ? 1 : 0;

                var (spawnRequestWM, spawnRequestUnmasked) = leverManagerSingleton.RequestSpawn(spawnMasked, toSpawn);
                levelSpawners[i].Spawn(spawnRequestWM, spawnRequestUnmasked);
            }
            currentPullRate = currentTime + pullRate;
        }


    }
}
