using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private bool musicState = true;
    private bool soundState = true;

    public delegate void EventSoundHandler();
    
    public event EventSoundHandler SoundChangedEvent;
    public event EventSoundHandler MusicChangedEvent;


    private void Awake()
    {
        if (instance == null)
        {
            // Keep this object alive between all the scenes
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            // if exists another instance of SceneManager, destroy this one
            Destroy(gameObject);
        }
    }

    public bool musicStateRead()
    {
        return musicState;
    }

    public bool soundStateRead()
    {
        return soundState;
    }

    public void changeMusicState()
    {
        musicState = !musicState;
        MusicChangedEvent?.Invoke();      
    }

    public void changeSoundState()
    {
        soundState = !soundState;
        SoundChangedEvent?.Invoke();
    }


    
}
