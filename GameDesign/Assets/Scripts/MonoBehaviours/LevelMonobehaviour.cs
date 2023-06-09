using System;
using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMonobehaviour : MonoBehaviour
{
    #region Attributes
    private LevelSettingManager manager = LevelSettingManager.Instance;
    [SerializeField] private TimerManagerMonoBehaviour timerManager;

    [SerializeField] int levelTime;
    [Range(0, 1)]
    [SerializeField] float[] wavePercentages;
    [SerializeField] float waitTime;
    [SerializeField] int maskedCustomers = 0;

    [SerializeField]
    public EnumDataContainer<int, CustomerTypes> unmaskedCustomers;

    [SerializeField]
    public EnumDataContainer<GameObject, CustomerTypes> unmaskedPrefabs;
    [SerializeField]
    public EnumDataContainer<GameObject, CustomerTypes> maskedPrefabs;

    [SerializeField]
    public EnumDataContainer<float, CustomerTypes> maskedPercentages;

    Dictionary<CustomerTypes, int> unmaskedDictionary = new Dictionary<CustomerTypes, int>();
    Dictionary<CustomerTypes, GameObject> unmaskedPrefabsDictionary = new Dictionary<CustomerTypes, GameObject>();
    Dictionary<CustomerTypes, GameObject> maskedPrefabsDictionary = new Dictionary<CustomerTypes, GameObject>();
    Dictionary<CustomerTypes, float> maskedWeightsDictionary = new Dictionary<CustomerTypes, float>();





    public static bool TimeHasStarted { get; private set; }


    [SerializeField] private Animator UIanimator;

    #endregion

    void Start()
    {
        TimeHasStarted = false;
        if (UIanimator == null) Debug.LogWarning("No UI animator Found");


        if (timerManager == null)
        {
            timerManager = GetComponent<TimerManagerMonoBehaviour>();
            if (timerManager == null)
            {
                timerManager = gameObject.AddComponent<TimerManagerMonoBehaviour>();
            }
        }
        timerManager.SetTime(levelTime);
        TimerManagerMonoBehaviour.OnTimeFinished += OnTimeFinished;
        BossAngerManager.OnAngryGameOver += OnTimeFinished;

        foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes)))
        {
            unmaskedDictionary.Add(t, unmaskedCustomers[(int)t]);
            unmaskedPrefabsDictionary.Add(t, unmaskedPrefabs[(int)t]);
            maskedPrefabsDictionary.Add(t, maskedPrefabs[(int)t]);
            maskedWeightsDictionary.Add(t, maskedPercentages[(int)t]);

        }
        manager.SetTotalSpawns(maskedCustomers, maskedWeightsDictionary, unmaskedDictionary);
        manager.SetPrefabs(unmaskedPrefabsDictionary, maskedPrefabsDictionary);
        manager.SetWaves(wavePercentages);



        StartCoroutine(StartLevel());

    }


    void OnDestroy()
    {
        TimerManagerMonoBehaviour.OnTimeFinished -= OnTimeFinished;
        BossAngerManager.OnAngryGameOver -= OnTimeFinished;
    }

    void Update()
    {

        //Just update to be seen in the editor.
        //so whenever there are changes we see them
        maskedCustomers = manager.CustomersToBeSpawnedWM;

        foreach (KeyValuePair<CustomerTypes, int> pair in manager.CustomersWithOutMask)
        {
            unmaskedCustomers[((int)pair.Key)] = pair.Value;
        }
    }

    bool isPointManagerReady()
    {
        return PointsManager.Instance.isReady;
    }

    bool isTaserManagerReady()
    {
        return TaserManager.Instance.isReady;
    }

    bool isBossManagerReady()
    {
        return BossAngerManager.Instance.isReady;
    }

    private IEnumerator waitLevel(float duration)
    {
        Debug.Log("Start waiting for characters...");

        yield return new WaitForSeconds(duration);

        ScenesManager.levelIsReady = true;

        Debug.Log("Ready to do the Countdown");

        UIanimator.SetTrigger("TriggerPlay");

        yield return new WaitForSecondsRealtime(5.30f); // Wait for the CountDown animation finish

        timerManager.StartTimer();
        TimeHasStarted = true;
        Debug.Log("Ready to play");

    }

    IEnumerator StartLevel()
    {
        Debug.Log("Waiting to start the level");
        yield return new WaitUntil(() => manager.SanityCheck() && isPointManagerReady() && isTaserManagerReady() && isBossManagerReady());
        Debug.Log("All managers are ready");
        StartCoroutine(waitLevel(waitTime));

    }
    void OnTimeFinished()
    {
        Debug.Log("Time's over! Level finished");
        Time.timeScale = 1;
        LevelSettingManager.invokeGameOverEvent();
    }




}
