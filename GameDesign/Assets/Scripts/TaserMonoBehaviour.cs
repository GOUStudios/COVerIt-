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

    [ReadOnly][SerializeField] float charge;

    void Start()
    {
        taserManager.taserCost = taserCostPerShot;
        taserManager.chargePerSecond = chargePerSecond;
        InvokeRepeating("chargeBattery", 1.0f, 1.0f);
    }
    void Update()
    {
        charge = taserManager.taserBatteryPercent;
    }

    private void chargeBattery()
    {

        if (taserManager.taserBattery <= taserManager.taserMaxBattery)
        {
            taserManager.taserBattery += taserManager.chargePerSecond;
            if (taserManager.taserBattery > taserManager.taserMaxBattery)
            {
                taserManager.taserBattery = taserManager.taserMaxBattery;
            }else if (taserManager.taserBattery < 0) {
                taserManager.taserBattery = 0;
            }
            Debug.Log($"Taser Battery = {taserManager.taserBattery}");
        }
    }
}

