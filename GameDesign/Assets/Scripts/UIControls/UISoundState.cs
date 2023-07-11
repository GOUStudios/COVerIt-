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
        if (toggle == null)
            toggle = GetComponent<Toggle>();

        float vol;

        if (audioMixer.GetFloat("VolumeVFX", out vol))
        {
            toggle.isOn = vol <= -60.0f;
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
    void Awake()
    {
        float vol;

        if (audioMixer.GetFloat("VolumeVFX", out vol))
        {
            if (toggle == null) toggle = GetComponent<Toggle>();
            toggle.isOn = vol <= -60.0f;
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
