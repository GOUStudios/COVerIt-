using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcoSpawner : MonoBehaviour
{
    [SerializeField][Range(0, 100)] float chanceOfSpawnage = 0.001f;
    void Start()
    {
        if (chanceOfSpawnage < Random.Range(0f, 100f))
        {
            Destroy(gameObject);
        }
    }
}
