using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReact : MonoBehaviour
{
    private AudioSource[] audioSources;
    

    void Start()
    {
       
        audioSources = GetComponents<AudioSource>();
        if (audioSources == null) return;
        Debug.Log("Searching for Audio Sources");
        // Check if there are some audio source component attached to the game object
        if (audioSources.Length > 0)
        {
            Debug.Log("AudioSource found:" + audioSources.Length);

            if(!SoundManager.Instance.soundStateRead() && gameObject.CompareTag("Sound"))
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.mute = true;
                }
            else if(!SoundManager.Instance.musicStateRead() && gameObject.CompareTag("Music"))
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.mute = true;
                }

            if (gameObject.CompareTag("Music"))
            {
                Debug.Log("Event music subsribed by: " + gameObject.name);
                SoundManager.Instance.MusicChangedEvent += ChangeState;
            }
            else if (gameObject.CompareTag("Sound"))
            {
                Debug.Log("Event sound subsribed by: " + gameObject.name);
                SoundManager.Instance.SoundChangedEvent += ChangeState;
            }



        }
        else
        {
            Debug.Log("No AudioSource found.");
        }
    }

    private void OnDestroy()
    {
        if (gameObject.CompareTag("Music"))
        {
            SoundManager.Instance.MusicChangedEvent -= ChangeState;
        }else if (gameObject.CompareTag("Sound"))
        {
            SoundManager.Instance.SoundChangedEvent -= ChangeState;
        }



    }


    private void ChangeState()
    {
        Debug.Log("Audio Reaction");

        if (audioSources.Length > 0 && audioSources != null)
        {
            Debug.Log("Changing State Audio");
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.mute = !audioSource.mute;
            }

        }
    }

}
