using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCharacterHandler : MonoBehaviour
{

    public void Hit()
    {
        Debug.Log($"This is the moment {name} got hit");
    }

    public void SpawnSmoke()
    {
        Debug.Log($"This is the moment {name} Spwaned smoke");
    }
    public void StartShocking()
    {
        Debug.Log($"This is the moment {name} Started being shocked");
    }

}
