using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerRotatorUI : MonoBehaviour
{
    private Slider slider;

    private Quaternion initialRotation;

    private void Start()
    {
        slider = GetComponentInParent<Slider>();
        if (slider == null) return;
        
        // Check if slider is on 0, TBD on Angry manager
        slider.value = 0f;

        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (slider == null) return;

            float rotationAmount = Mathf.Lerp(0f, 95f, slider.value); // Calculate rotation angle in function of slider value
            transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, rotationAmount);

    }

}
