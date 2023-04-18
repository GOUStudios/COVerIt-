using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollideDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"Collided with {collider.gameObject.name}");
        Destroy(collider.gameObject);
        Destroy(gameObject);
    }
}
