using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAlarm : MonoBehaviour
{
    private Image redBoundaries;

    public float minFrequency = 1.0f;
    public float maxFrequency = 10.0f;

    private bool isBlinking = false;
    private float frequency = 0.5f;
    private float elapsedTime = 0.0f;


    private void Start()
    {
        redBoundaries = GetComponent<Image>();

        if (redBoundaries == null)
        {
            Debug.LogError("Il riferimento all'oggetto Image non � stato assegnato!");
        }

        var tempColor = redBoundaries.color;
        tempColor.a = 0f;
        redBoundaries.color = tempColor;

    }

    private void Update()
    {
        // Controlla se l'immagine deve iniziare a blinkare
        if (isBlinking)
        {
            elapsedTime += Time.deltaTime;

            // Calcola il valore alpha per il blink utilizzando la frequenza
            float alpha = Mathf.PingPong(elapsedTime * frequency, 1.0f);

            // Aggiorna il valore alpha dell'immagine
            Color imageColor = redBoundaries.color;
            imageColor.a = alpha;
            redBoundaries.color = imageColor;
        }
    }

    public void StartBlink()
    {
        Debug.Log("Event angry");

        if (!isBlinking)
        {
            frequency = Mathf.Lerp(minFrequency, maxFrequency, frequency);
            elapsedTime = 0.0f;
            isBlinking = true;
        }
    }

    public void StopBlink()
    {
        isBlinking = false;

        // Reimposta il valore alpha dell'immagine a 1
        Color imageColor = redBoundaries.color;
        imageColor.a = 1.0f;
        redBoundaries.color = imageColor;
    }

}
