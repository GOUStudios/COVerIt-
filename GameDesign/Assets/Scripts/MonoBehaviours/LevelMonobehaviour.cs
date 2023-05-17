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
    [SerializeField] int maskedCustomers = 0;
    [SerializeField] int levelTime;

    [Header("DO NOT CHANGE THE ORDER OF THE LIST")]
    [EnumNamedArray(typeof(CustomerTypes))]
    public int[] unmaskedCustomers = new int[System.Enum.GetValues(typeof(CustomerTypes)).Length];

    Dictionary<CustomerTypes, int> unmaskedDictionary = new Dictionary<CustomerTypes, int>();

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
        }
        manager.setTotalSpawns(maskedCustomers, unmaskedDictionary);
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
    }
}
