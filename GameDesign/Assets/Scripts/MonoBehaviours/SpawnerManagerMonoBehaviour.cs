using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagerMonoBehaviour : MonoBehaviour
{

    [SerializeField] LevelSettingManager leverManagerSingleton;
    [SerializeField] TimerManagerMonoBehaviour timerManager;

    [Header("Wave properties")]
    [SerializeField] EnumDataContainer<AnimationCurve, CustomerTypes> spawnRatesArray;
    [Range(0.0f, 10f)]
    [SerializeField] int WaveMultiplier;

    [Header("Spawner rates")]

    [SerializeField] AnimationCurve maskedSpawnRate;
    [Range(0.0f, 5.0f)]
    [SerializeField] float PullRate;
    [SerializeField] SpawnerWaypoint[] levelSpawners;


    [Header("Despawner properties")]
    [SerializeField] public float DespawnAfterSeconds;

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
        TimerManagerMonoBehaviour.OnWaveStart += OnWaveStart;

        var existingSpawners = FindObjectsOfType<SpawnerWaypoint>();
        if (existingSpawners.Length > levelSpawners.Length)
        {
            Debug.LogWarning("The SpawnerManager is not aware of all the spawners in the scene! Trying to fix...");
            levelSpawners = existingSpawners;
        }
    }
    void OnDestroy()
    {
        TimerManagerMonoBehaviour.OnWaveStart -= OnWaveStart;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        float timePercentage = currentTime / timerManager.GetTime();
        if (currentTime >= currentPullRate && timePercentage < 1f)
        {
            determineSpawn(levelSpawners.Length);
            currentPullRate = currentTime + PullRate;
        }
    }

    private void determineSpawn(int baseCount = 1, bool overrideProbability = false, float overridenProbability = 0.0f)
    {
        float timePercentage = currentTime / timerManager.GetTime();
        float p = overrideProbability ? overridenProbability : UnityEngine.Random.Range(0f, 1.01f);

        Dictionary<CustomerTypes, int> toSpawnUnmasked = new Dictionary<CustomerTypes, int>();
        foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes)))
        {
            if (spawnRates[t].Evaluate(timePercentage) >= p)
            {
                int toAdd;
                if (overrideProbability) toAdd = baseCount;
                else toAdd = UnityEngine.Random.Range(0, baseCount) + 1;
                toSpawnUnmasked.Add(t, toAdd);
            }
        }

        p = overrideProbability ? overridenProbability : UnityEngine.Random.Range(0f, 1.01f);
        int spawnMasked = 0;
        if (maskedSpawnRate.Evaluate(timePercentage) >= p)
        {
            if (overrideProbability) spawnMasked = baseCount;
            else spawnMasked = UnityEngine.Random.Range(0, baseCount) + 1;
        }
        var (spawnRequestWM, spawnRequestUnmasked) = leverManagerSingleton.RequestSpawn(spawnMasked, toSpawnUnmasked);
        // Debug.Log($"Received request answer with {spawnRequestWM} and {ObjectUtils.DictionaryToString(spawnRequestUnmasked)}");

        for (int i = 0; i < levelSpawners.Length/*toSpawnUnmasked.Length*/; i++)
        {
            Dictionary<CustomerTypes, int> toSpawn = new Dictionary<CustomerTypes, int>();
            foreach (var t in spawnRequestUnmasked.Keys)
            {
                var randomPartitions = RandomUtils.GetRandomPartitions(levelSpawners.Length, spawnRequestUnmasked[t]);
                toSpawn.Add(t, randomPartitions[i]);
            }
            var randomWMPartitions = RandomUtils.GetRandomPartitions(levelSpawners.Length, spawnRequestWM);
            levelSpawners[i].Spawn(randomWMPartitions[i], toSpawn);
        }
    }

    void OnWaveStart(int wavesRemaining)
    {
        determineSpawn(wavesRemaining > 0 ? WaveMultiplier * levelSpawners.Length : 999, wavesRemaining < 2);
    }
}
