using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private bool musicState = true;
    private bool soundState = true;

    public delegate void EventSoundHandler();
    
    public event EventSoundHandler SoundChangedEvent;
    public event EventSoundHandler MusicChangedEvent;


    private void Awake()
    {
        
        if (Instance == null)
        {
            // Keep this object alive between all the scenes
            DontDestroyOnLoad(gameObject);
            Instance = this;
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
        Debug.Log("invoked music state change");
        musicState = !musicState;
        MusicChangedEvent?.Invoke();      
    }

    public void changeSoundState()
    {
        Debug.Log("invoked sound state change");
        soundState = !soundState;
        SoundChangedEvent?.Invoke();
    }


    
}
