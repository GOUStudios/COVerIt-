using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    public AudioSource Beep1;
    public AudioSource Beep2;
    public AudioSource Beep3;
    public AudioSource BeepStart;

    public void Playbeep1()
    {
        if( Beep1 == null) return;

        Beep1.Play();

    }
    
    public void Playbeep2()
    {
        if( Beep2 == null) return;

        Beep2.Play();

    }


    public void Playbeep3()
    {
        if (Beep3 == null) return;

        Beep3.Play();

    }

    public void PlaybeepStart()
    {
        if (BeepStart == null) return;

        BeepStart.Play();

    }
}
