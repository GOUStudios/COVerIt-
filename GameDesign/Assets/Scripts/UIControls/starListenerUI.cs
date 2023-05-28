using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starListenerUI : MonoBehaviour
{
    private StarLauncherUI eventLauncher;
    private Animator animatorController;

    public float valueResponse;

    // Start is called before the first frame update
    void Start()
    {
        eventLauncher = GetComponentInParent<StarLauncherUI>();

        eventLauncher.starEvent += StarSpawner;

        animatorController = GetComponent<Animator>();
    }


    private void StarSpawner(float value)
    {
        Debug.Log("valueResponse: " + valueResponse + "value " + value);

        if (valueResponse == value && !animatorController.GetBool("StarGained"))
        {
            Debug.Log("inside valueResponse: " + valueResponse + "value " + value);
            animatorController.SetBool("StarGained", true);
        }
    }
}
