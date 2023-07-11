using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlarm : MonoBehaviour
{
    private Image redBoundaries;
    private AudioSource audioAlarm;

    public float minFrequency = 1.0f;
    public float maxFrequency = 10.0f;

    private bool isBlinking = false;
    private float frequency = 2.5f;
    private float elapsedTime = 0.0f;


    private void Start()
    {
        redBoundaries = GetComponent<Image>();


        if (redBoundaries == null)
        {
            Debug.LogError("Il riferimento all'oggetto Image non è stato assegnato!");
        }

        audioAlarm= GetComponent<AudioSource>();
        audioAlarm.Stop();

        var tempColor = redBoundaries.color;
        tempColor.a = 0f;
        redBoundaries.color = tempColor;


        BossAngerManager.OnAngryGameOver += StopBlink;
    }

    private void Update()
    {
        
        if (isBlinking)
        {
            elapsedTime += Time.deltaTime;

            
            float alpha = Mathf.PingPong(elapsedTime * frequency, 1.0f);

            
            Color imageColor = redBoundaries.color;
            imageColor.a = alpha;
            redBoundaries.color = imageColor;
        }
    }

    public void StartBlink()
    {
        Debug.Log("Start blinking");

        if (!isBlinking)
        {
            frequency = Mathf.Lerp(minFrequency, maxFrequency, frequency);
            elapsedTime = 0.0f;
            isBlinking = true;
            if (audioAlarm != null) audioAlarm.Play();
        }
       
    }

    public void StopBlink()
    {
        Debug.Log("Stop blinking");
        isBlinking = false;

   
        Color imageColor = redBoundaries.color;
        imageColor.a = 0.0f;
        redBoundaries.color = imageColor;

        if (audioAlarm != null) audioAlarm.Stop();
    }

}
