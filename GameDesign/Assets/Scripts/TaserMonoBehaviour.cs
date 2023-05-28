using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserMonoBehaviour : MonoBehaviour
{
    // This class is only required to change the charge of the taser
    [SerializeField] public TaserManager taserManager = TaserManager.Instance;

    void Start()
    {
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

