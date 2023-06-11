using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager
{
    private static TaserManager instance;

    public static TaserManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TaserManager();
            }
            return instance;
        }
    }

    //Taser variables
    public int taserBattery;
    public int taserCost = 20;
    public int taserMaxBattery { get; private set; }
    public int chargePerSecond = 2;
    public bool isReady = false;



    public float taserBatteryPercent
    {
        get
        {
            return ((float)taserBattery / (float)taserMaxBattery);
        }
    }

    private TaserManager()
    {
        taserMaxBattery = 100;
        taserBattery = taserMaxBattery;
    }

    public bool useTaser()
    {
        if (taserBattery >= taserCost)
        {
            taserBattery = taserBattery - taserCost;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void chargeBattery()
    {
        if (taserBattery < taserMaxBattery)
        {
            taserBattery += chargePerSecond;
            Debug.Log($"Taser Battery = {taserBattery}");
        }
    }

}
