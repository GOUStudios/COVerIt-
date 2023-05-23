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
    public int taserMaxBattery = 100;
    public bool triggerIsFree = true;
    public int chargePerSecond = 2;

    private TaserManager()
    {
        taserBattery = taserMaxBattery;
    }

    public void taserTrigger(){
        if(taserCanTrigger()){
            taserBattery -= taserCost;
        }
    }

    public bool taserCanTrigger(){
        if (taserBattery >= taserMaxBattery && triggerIsFree)
        {
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

    private float timer;
    private float interval = 1.0f; // Intervalo de tiempo en segundos

    public void Start()
    {
        // Iniciar el temporizador
        timer = interval;
        Debug.Log($"START TASER SINGLETON");
    }

    public void Update()
    {
        // Actualizar el temporizador
        timer -= Time.deltaTime;

        // Verificar si ha pasado el intervalo de tiempo
        if (timer <= 0)
        {
            // Ejecutar la función deseada
            chargeBattery();

            // Reiniciar el temporizador
            timer = interval;
        }
    }

}
