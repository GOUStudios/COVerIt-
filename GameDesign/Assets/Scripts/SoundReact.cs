using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReact : MonoBehaviour
{
    private AudioSource[] audioSources;
    private SoundManager manager;

    void Start()
    {
        manager = FindObjectOfType<SoundManager>();

        AudioSource[] audioSources = GetComponents<AudioSource>();

        // Check if there are some audio source component attached to the game object
        if (audioSources.Length > 0)
        {
            Debug.Log("AudioSource found:");

            if(!manager.musicStateRead() && gameObject.CompareTag("Sound"))
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.mute = true;
                }
            else if(!manager.soundStateRead() && gameObject.CompareTag("Music"))
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.mute = true;
                }

            if (gameObject.CompareTag("Music"))
            {
               // manager.MusicChangedEvent += ChangeState;
            }

            if (gameObject.CompareTag("Sound"))
            {
                //manager.SoundChangedEvent += ChangeState;
            }

        }
        else
        {
            Debug.Log("No AudioSource found.");
        }
    }

    /*
    public void ChangeState()
    {
        Debug.Log("Audio Reaction");
        if (audioSources.Length > 0)
        {
            Debug.Log("Changing State Audio");
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = !audioSource.mute;
            }

        }
    }*/

}
