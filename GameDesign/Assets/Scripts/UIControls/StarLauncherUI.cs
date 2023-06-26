using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StarLauncherUI : MonoBehaviour
{
    public Slider slider;

    private Animator starsAnimator;
    private PointsManager pointsManager;//has to be initialized in awake because is a monobehaviour.

    private void Start()
    {
        pointsManager = PointsManager.Instance;

        starsAnimator = GetComponentInChildren<Animator>();

        slider = GetComponent<Slider>();
        slider.value = 0;

        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(slider.value); });
    }


    private void Update()
    {
        slider.value = pointsManager.pointsPercentage;
    }
    public void OnSliderValueChanged(float value)
    {
        if (starsAnimator == null) return;

        starsAnimator.SetInteger("stars", pointsManager.GetNumberStars());

    }
}

