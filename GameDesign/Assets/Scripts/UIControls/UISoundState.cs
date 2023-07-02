using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundState : MonoBehaviour
{
    private Toggle toggle;


    // Start is called before the first frame update
    void Start()
    {

        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(ChangeStateRequest);

        if (SoundManager.Instance != null)
        {
            if (SoundManager.Instance.soundStateRead())
            {
                bool newIsOnState = false;
                toggle.SetIsOnWithoutNotify(newIsOnState);
            }
            else
            {
                bool newIsOnState = true;
                toggle.SetIsOnWithoutNotify(newIsOnState);
            }
        }
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveAllListeners();
    }


    private void ChangeStateRequest(bool value)
    {
        SoundManager.Instance.changeSoundState();
    }

}
