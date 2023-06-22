using UnityEngine;

public class BackgroundMusicMonoBehaviour : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 8f;  // Duration of the fade-in and fade-out in seconds

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float originalVolume;
    [SerializeField] private float targetVolume;
    [SerializeField] private float currentVolume;
    [SerializeField] public bool isFadingIn;
    [SerializeField] public bool isFadingOut;

    [SerializeField] private float fadeTimer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
        targetVolume = originalVolume;
        audioSource.volume = 0f;  // Start with the volume set to 0

        audioSource.Play();

        FadeIn();

        //Invoke("FadeOut", fadeDuration);
    }

    private void Update()
    {
        fadeTimer += Time.deltaTime;

        if (isFadingIn)
        {
            if (fadeTimer <= fadeDuration)
            {
                // Calculate the current volume based on the fade progress
                currentVolume = Mathf.Lerp(0f, targetVolume, fadeTimer / fadeDuration);
                audioSource.volume = currentVolume;
            }
            else
            {
                audioSource.volume = targetVolume;
                isFadingIn = false;
            }
        }

        if (isFadingOut)
        {
            if (fadeTimer <= fadeDuration)
            {
                // Calculate the current volume based on the fade progress
                currentVolume = Mathf.Lerp(originalVolume, targetVolume, fadeTimer / fadeDuration);
                audioSource.volume = currentVolume;
            }
            else
            {
                audioSource.volume = 0f;
                isFadingOut = false;
            }
        }
    }

    public void FadeOut()
    {
        targetVolume = 0f;
        fadeTimer = 0f;
        isFadingOut = true;
    }

    public void FadeIn()
    {
        targetVolume = originalVolume;
        fadeTimer = 0f;
        isFadingIn = true;
    }

    public void PlaySound(){
        audioSource.Play();
    }

    public void StopSound(){
        audioSource.Stop();
    }

    public void Mute(){
        audioSource.volume = 0f;
    }

    public void SetVolume(float multiplier){
        audioSource.volume = originalVolume*multiplier;
    }
}
