using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMonobehaviour : MonoBehaviour
{
    #region Attributes
    private LevelSettingManager manager = LevelSettingManager.Instance;
    [SerializeField] int maskedCustomers = 0;


    [EnumNamedArray(typeof(CustomerTypes))]
    public int[] unmaskedCustomers = new int[System.Enum.GetValues(typeof(CustomerTypes)).Length];

    #endregion

    void Start()
    {
        manager.setTotalSpawns(maskedCustomers, unmaskedCustomers);
    }

    void Update()
    {

        //Just update to be seen in the editor.
        //so whenever there are changes we see them
        maskedCustomers = manager.CustomersToBeSpawnedWM;
        for (int i = 0; i < unmaskedCustomers.Length; i++)
        {
            unmaskedCustomers[i] = manager.CustomersWithOutMask[i];
        }
    }
}
