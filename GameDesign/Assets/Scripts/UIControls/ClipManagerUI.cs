using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class ClipManagerUI : MonoBehaviour
{
    public VideoPlayer[] VideoPlayer;

    private VideoPlayer _player1;
    private VideoPlayer _player2;
    

    private Vector2[] timeIntervals;

    private bool isCheckingTime = false;

    public BackgroundMusicMonoBehaviour backgroundMusic; 

    private void Start()
    {
        _player1 = VideoPlayer[0];
        _player2 = VideoPlayer[1];

        //Inizialization for time interval for clip changes
        timeIntervals = new Vector2[]
        {
            new Vector2(00.33f, 00.66f),
            new Vector2(01.53f, 02.59f),
            new Vector2(03.86f, 05.92f),
            new Vector2(08.26f, 09.40f),
            new Vector2(10.69f, 13.86f),
            new Vector2(14.53f, 26.59f),
            new Vector2(29.69f, 30.72f),
            new Vector2(32.23f, 33.23f)
        };


    }

    public void onClick()
    {
        if (_player1 == null || _player2 == null) return;

        if (!isCheckingTime)
        {
            backgroundMusic.StopSound();

            // Starts coroutine to check interval
            StartCoroutine(CheckTimeInterval());
        }
    }


    //Check if the main clip is in the right interval to do a clip changes
    private System.Collections.IEnumerator CheckTimeInterval()
    {
        isCheckingTime = true;

        while (_player1.isPlaying)
        {
            float currentSeconds = (float)_player1.time;

            
            for (int i = 0; i < timeIntervals.Length; i++)
            {
                float startTime = timeIntervals[i].x;
                float endTime = timeIntervals[i].y;

                if (currentSeconds >= startTime && currentSeconds <= endTime)
                {
                    
                    clip2();
                    isCheckingTime = false;
                    yield break;
                }
            }

            yield return null;
        }

        isCheckingTime = false;
    }

    private void clip2()
    {
        _player2.targetCameraAlpha = 1.0f;
        _player2.Play();
        _player1.Pause();

        Debug.Log("Play clip2()");
    }
}

