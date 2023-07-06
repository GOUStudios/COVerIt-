using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BatteryUI : MonoBehaviour
{
    [Header("UI elements")]
    //Controls the battery levels
    [SerializeField] private Image battery3;
    [SerializeField] private Image battery2;
    [SerializeField] private Image battery1;

    //Control the taser avalaible image
    [SerializeField] private Image taserOn;
    [SerializeField] private Image taserOff;

    TaserManager taserManager = TaserManager.Instance;

    private float Threshold1 = 0.66f;
    private float Threshold2 = 0.33f;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = taserManager.taserBatteryPercent;

        //Set on all battery charges 
        battery3.enabled = true;
        battery2.enabled = true;
        battery1.enabled = true;

        //Set the initial taser PNG image
        taserOn.enabled = true;
        taserOff.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = taserManager.taserBatteryPercent;


        if (battery1 == null || battery2 == null || battery3 == null) return;
        switch (slider.value)
        {
            case float n when n <= Threshold1 && n > Threshold2:
                //Debug.Log("2 charges");
                battery3.enabled = false;
                battery2.enabled = true;
                battery1.enabled = true;

                taserOn.enabled = true;
                taserOff.enabled = false;
                break;

            case float n when n <= Threshold2 && n != 0 && n >= taserManager.taserCostPercent:
                //Debug.Log("1 charges");
                battery3.enabled = false;
                battery2.enabled = false;
                battery1.enabled = true;

                taserOn.enabled = true;
                taserOff.enabled = false;
                break;

            case float n when n < taserManager.taserCostPercent || n <= 0:
                //Debug.Log("0 charges");
                battery3.enabled = false;
                battery2.enabled = false;
                battery1.enabled = false;

                taserOn.enabled = false;
                taserOff.enabled = true;
                break;

            default:
                //Debug.Log("3 charges");
                battery3.enabled = true;
                battery2.enabled = true;
                battery1.enabled = true;

                taserOn.enabled = true;
                taserOff.enabled = false;
                break;
        }

    }
}

