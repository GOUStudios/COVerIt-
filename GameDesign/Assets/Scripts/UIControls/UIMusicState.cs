using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class UIMusicState : MonoBehaviour
{
    private Toggle toggle;
    [SerializeField] AudioMixer audioMixer;


    // Start is called before the first frame update
    void Start()
    {

        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(ChangeStateRequest);

        float vol;

        if (audioMixer.GetFloat("VolumeMusic", out vol))
        {
            toggle.isOn = vol == -80f;
        }

        if (SoundManager.Instance != null)
        {
            if (SoundManager.Instance.musicStateRead())
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
            audioMixer?.SetFloat("VolumeMusic", -80f);
        else
            audioMixer?.SetFloat("VolumeMusic", 0);

    }

    private void ChangeStateRequest(bool value)
    {
        SoundManager.Instance.changeMusicState();
    }

}
