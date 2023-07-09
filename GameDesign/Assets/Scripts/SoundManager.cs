using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject GO = new GameObject("SoundManager");
                _instance = GO.AddComponent<SoundManager>();
            }
            return _instance;
        }
    }


    private bool musicState = true;
    private bool soundState = true;

    public delegate void EventSoundHandler();

    public static event EventSoundHandler SoundChangedEvent;
    public static event EventSoundHandler MusicChangedEvent;


    private void Awake()
    {

        if (_instance == null)
        {
            // Keep this object alive between all the scenes
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else if (_instance != null && _instance != this)
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
        Debug.Log("music state: " + musicState);
        MusicChangedEvent?.Invoke();
    }

    public void changeSoundState()
    {
        Debug.Log("invoked sound state change");
        soundState = !soundState;
        Debug.Log("Sound state: " + soundState);
        SoundChangedEvent?.Invoke();
    }



}
