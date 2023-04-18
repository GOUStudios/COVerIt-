using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnType;
    [SerializeField] private float distanceFromCamera = 10;

    public GameObject SpawnMask(Ray direction, GameObject target)
    {
        Debug.DrawRay(direction.origin, direction.direction.normalized * (target.transform.position - direction.origin).magnitude, Color.red, 10);
        var mask = Instantiate(spawnType, direction.origin + direction.direction.normalized * distanceFromCamera, Quaternion.Euler(direction.direction));
        MaskController m = mask.GetComponent<MaskController>();
        m?.SetTarget(target);
        return mask;
    }
}
