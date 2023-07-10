using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    //Level between 0 and 1 to show the anger boss
    [ReadOnly][SerializeField] public float angerLevel;//Calculated by the boss manager
    [ReadOnly][SerializeField] private float angerValue;

    [SerializeField] private Image boss;
    [SerializeField] private Image bossAnger;

    private Slider slider;

    private BossAngerManager bossAngerManager = BossAngerManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        angerLevel = BossAngerManager.Instance.angerThreshhold;

        slider = GetComponent<Slider>();

        //slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    public void CheckAnger()
    {
        if (!BossAngerManager.Instance.isAngry)
        {
            boss.enabled = true;
            bossAnger.enabled = false;
        }
        else
        {
            boss.enabled = false;
            bossAnger.enabled = true;
        }

    }

    private void Update()
    {
        slider.value = 1-bossAngerManager.angerPercent;
        angerValue = bossAngerManager.angerPercent;
        CheckAnger();
    }

}
