using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StarLauncherUI : MonoBehaviour
{
    public Slider slider;
    public event Action<float> starEvent;

    [ReadOnly][SerializeField] private bool event1Triggered = false;
    [ReadOnly][SerializeField] private bool event2Triggered = false;
    [ReadOnly][SerializeField] private bool event3Triggered = false;

    private void Start()
    {
        
        slider= GetComponent<Slider>();
        slider.value = 0;

        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(slider.value); });
    }

    private void Update()
    {
        slider.value = slider.value + 0.0001f;
    }
    public void OnSliderValueChanged(float value)
    {

        if (value >= 0.32f && !event1Triggered)
        {
            event1Triggered = true;
            Debug.Log("Event triggered for value 0.32!");

            // Esegui le azioni desiderate per l'evento 0.32 qui
            starEvent.Invoke(Mathf.Round(value * 100f) / 100f);
        }
        else if (value >= 0.66f && !event2Triggered)
        {
            event2Triggered = true;
            Debug.Log("Event triggered for value 0.66!");

            // Esegui le azioni desiderate per l'evento 0.66 qui
            starEvent.Invoke(Mathf.Round(value * 100f) / 100f);
        }
        else if (value == 1f && !event3Triggered)
        {
            event3Triggered = true;
            Debug.Log("Event triggered for value 1!");

            // Esegui le azioni desiderate per l'evento 1 qui
            starEvent.Invoke(Mathf.Round(value * 100f) / 100f);
        }
    }
}

