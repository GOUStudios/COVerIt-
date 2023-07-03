using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BackgroundObjectMonoBehaviour : MonoBehaviour, Clickable
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip miss;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(ClickType clickType)
    {
        onHitBehaviour();
    }

    protected virtual void onHitBehaviour()
    {
        ClickManager.Instance.onCorrectlyClickInvoke();
        audioSource.PlayOneShot(miss, 0.7f);
    }

}
