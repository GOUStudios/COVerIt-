using UnityEngine;

public class BackgroundMusicMonoBehaviour : MonoBehaviour
{
    public float fadeDuration = 2f;  // Duration of the fade-in and fade-out in seconds

    // Remember to add the audio source to the component
    private AudioSource audioSource;
    private float originalVolume;
    private float targetVolume;
    private float fadeTimer;
    private bool isFading;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
        targetVolume = originalVolume;
        audioSource.volume = 0f;  // Start with the volume set to 0

        FadeIn();
    }

    private void Update()
    {
        fadeTimer += Time.deltaTime;

        if (fadeTimer <= fadeDuration && isFading)
        {
            // Calculate the current volume based on the fade progress
            float currentVolume = Mathf.Lerp(0f, targetVolume, fadeTimer / fadeDuration);
            audioSource.volume = currentVolume;
        }
    }

    public void FadeOut()
    {
        if (!isFading)
        {
            targetVolume = 0f;
            fadeTimer = 0f;
            isFading = true;
        }
    }

    public void FadeIn()
    {
        if (isFading)
        {
            return;
        }

        audioSource.volume = 0f;
        audioSource.Play();
        targetVolume = originalVolume;
        fadeTimer = 0f;
        isFading = true;
    }
}