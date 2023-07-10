using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class UISoundState : MonoBehaviour
{
    private Toggle toggle;
    [SerializeField] AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {

        toggle = GetComponent<Toggle>();
        float vol;

        if (audioMixer.GetFloat("VolumeVFX", out vol))
        {
            toggle.isOn = vol == -80f;
        }


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

    public void toggleAudio()
    {
        if (toggle.isOn)
            audioMixer?.SetFloat("VolumeVFX", -80f);
        else
            audioMixer?.SetFloat("VolumeVFX", 0);

    }

    private void ChangeStateRequest(bool value)
    {
        SoundManager.Instance.changeSoundState();
    }

}
