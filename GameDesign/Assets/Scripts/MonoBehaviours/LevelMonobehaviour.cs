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

    [Header("DO NOT CHANGE THE ORDER OF THE LIST")]
    [SerializeField] int maskedCustomers = 0;
    [EnumNamedArray(typeof(CustomerTypes))]
    public int[] unmaskedCustomers = new int[System.Enum.GetValues(typeof(CustomerTypes)).Length];

    [EnumNamedArray(typeof(CustomerTypes))]
    public GameObject[] unmaskedPrefabs = new GameObject[System.Enum.GetValues(typeof(CustomerTypes)).Length];
    [EnumNamedArray(typeof(CustomerTypes))]
    public GameObject[] maskedPrefabs = new GameObject[System.Enum.GetValues(typeof(CustomerTypes)).Length];
    
    [EnumNamedArray(typeof(CustomerTypes))]
    public float[] maskedPercentages = new float[System.Enum.GetValues(typeof(CustomerTypes)).Length];

    Dictionary<CustomerTypes, int> unmaskedDictionary = new Dictionary<CustomerTypes, int>();
    Dictionary<CustomerTypes, GameObject> unmaskedPrefabsDictionary = new Dictionary<CustomerTypes, GameObject>();
    Dictionary<CustomerTypes, GameObject> maskedPrefabsDictionary = new Dictionary<CustomerTypes, GameObject>();
    Dictionary<CustomerTypes, float> maskedWeightsDictionary = new Dictionary<CustomerTypes, float>();

    #endregion

    void Start()
    {
        if(timerManager == null)
        {
            timerManager = GetComponent<TimerManagerMonoBehaviour>();
            if(timerManager == null)
            {
                timerManager = gameObject.AddComponent<TimerManagerMonoBehaviour>();
            }
        }
        timerManager.SetTime(levelTime);
        TimerManagerMonoBehaviour.OnTimeFinished += OnTimeFinished;

        foreach (CustomerTypes t in Enum.GetValues(typeof(CustomerTypes)))
        {
            unmaskedDictionary.Add(t, unmaskedCustomers[(int)t]);
            unmaskedPrefabsDictionary.Add(t, unmaskedPrefabs[(int)t]);
            maskedPrefabsDictionary.Add(t, maskedPrefabs[(int)t]);
            maskedWeightsDictionary.Add(t, maskedPercentages[(int)t]);

        }
        manager.SetTotalSpawns(maskedCustomers, maskedWeightsDictionary, unmaskedDictionary);
        manager.SetPrefabs(unmaskedPrefabsDictionary, maskedPrefabsDictionary);
        timerManager.StartTimer();
    }

    void Update()
    {

        //Just update to be seen in the editor.
        //so whenever there are changes we see them
        maskedCustomers = manager.CustomersToBeSpawnedWM;
        foreach (KeyValuePair<CustomerTypes, int> pair in manager.CustomersWithOutMask)
        {
            unmaskedCustomers[(int)(pair.Key)] = pair.Value;
        }
    }

    void OnTimeFinished()
    {
        Debug.Log("Time's over! Level finished");
        Time.timeScale = 0;
    }
}
