using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    //Level between 0 and 1 to show the anger boss
    public float angerLevel;
    
    [SerializeField] private Image boss; 
    [SerializeField] private Image bossAnger;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    public void OnSliderValueChanged()
    {
        if(slider.value >= angerLevel)
        {
            boss.enabled= false;
            bossAnger.enabled= true;
        }
        else
        {
            boss.enabled = true;
            bossAnger.enabled= false;
        }
    }

    private void Update()
    {
        slider.value = slider.value + 0.0001f;
    }

}
