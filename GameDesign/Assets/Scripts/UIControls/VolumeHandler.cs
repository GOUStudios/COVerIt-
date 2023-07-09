using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeHandler : MonoBehaviour
{
    [SerializeField] AudioMixer m_mixer;
    [SerializeField] Slider m_audioSlider;

    void Start()
    {

        m_audioSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        setVolume();

    }
    public void setVolume()
    {
        float volume = Mathf.Log10(m_audioSlider.value) * 20;
        m_mixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", m_audioSlider.value);
    }
}
