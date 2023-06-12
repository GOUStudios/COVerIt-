using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCharacterHandler : MonoBehaviour
{

    public void Hit()
    {
    }

    public void SpawnSmoke()
    {
            VFXManager.Instance.spawnSmokeAt(transform, transform.position, transform.rotation);
    }
    public void StartShocking()
    {
    }

}
