using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventTriggerBoss : GameEventTrigger
{
    BossAngerManager m_BA_instance;

    void Awake()
    {
        m_BA_instance = BossAngerManager.Instance;
        ClickManager.OnMissClicked += TriggerEventViaCode;

    }

    void OnDestroy()
    {
        ClickManager.OnMissClicked -= TriggerEventViaCode;
    }
    protected override bool EventTriggerCondition(Collider other) => m_BA_instance.isAngry;
}
