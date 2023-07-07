using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameOverHidder : MonoBehaviour
{
    void Awake()
    {
        LevelSettingManager.OnGameOver += Hide;
    }
    void OnDestroy()
    {
        LevelSettingManager.OnGameOver -= Hide;
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
