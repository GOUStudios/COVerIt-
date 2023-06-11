using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAngerMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _maxNumAngry; //To be defined on each level

    [SerializeField]
    private int _maxNumGameOver; //To be defined on each level


    private BossAngerManager _instance;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = BossAngerManager.Instance;
        ClickManager.OnCorrectlyClicked += TriggerEvent_CorrectlyClicked;
        ClickManager.OnMissClicked += TriggerEvent_MissClicked;
    }

    void TriggerEvent_CorrectlyClicked()
    {
        Debug.Log($"CORRECTLY CLICKED IN {this}");
        _instance.TriggerEvent_CorrectlyClicked();
    }

    void TriggerEvent_MissClicked()
    {
        Debug.Log($"MISS CLIKED IN {this}");
        _instance.TriggerEvent_MissClicked();
    }
}
