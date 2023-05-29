using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//This Script changes provide the starting animation for the plates, changing the alpha of the materials and the height
public class fading : MonoBehaviour
{
    public float fadeDuration = 0.8f; // Second for fade
    private float currentAlpha = 0.0f; // Alpha 
    private bool fade = true;

    public float heightTarget;
    private float currentHeight;

     private Renderer[] childRenderers;

    private void Start()
    {
            
        childRenderers = GetComponentsInChildren<Renderer>();
        currentHeight = transform.position.y;
    }

    private void Update()
    {
        if (fade)
        {
        
            float t = Mathf.Clamp01(Time.deltaTime / fadeDuration);
            currentAlpha = Mathf.Lerp(currentAlpha, 1.0f, t);
            currentHeight = Mathf.Lerp(currentHeight, heightTarget, t);

            if (currentAlpha >= 1.0f - 0.01f) currentAlpha = 1.0f;
            if (currentHeight >= heightTarget - 0.01f) currentHeight = heightTarget;

            foreach (Renderer renderer in childRenderers)
            {
                foreach (Material material in renderer.materials)
                {
                    Color color = material.color;
                    color.a = currentAlpha;
                    material.color = color;
                    
                }
            }

            Vector3 position = transform.position;
            position.y = currentHeight;
            
            transform.position = position;
            

            // Controllare se il dissolvimento è completo
            if (currentAlpha >= 1.0f && Mathf.Approximately(currentHeight, heightTarget))
            {
                fade = false;
                Debug.Log("fade finish");

            }
        }

    }
}
