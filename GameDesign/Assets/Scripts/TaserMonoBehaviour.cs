using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserMonoBehaviour : MonoBehaviour
{
    // This class is only required to change the charge of the taser
    private TaserManager taserManager = TaserManager.Instance;

    [Range(0, 100)]
    [SerializeField] int taserCostPerShot;

    [Range(-100, 100)]
    [SerializeField] int chargePerSecond;

    void Start()
    {
        taserManager.taserCost = taserCostPerShot;
        taserManager.chargePerSecond = chargePerSecond;
        InvokeRepeating("chargeBattery", 1.0f, 1.0f);
    }

    private void chargeBattery()
    {
        if (taserManager.taserBattery < taserManager.taserMaxBattery)
        {
            taserManager.taserBattery += taserManager.chargePerSecond;
            Debug.Log($"Taser Battery = {taserManager.taserBattery}");
        }
    }
}

