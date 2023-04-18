using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnType;
    [SerializeField] private float distanceFromObject= 20f;

    public GameObject SpawnMask(Ray direction, GameObject target)
    {
        Debug.DrawRay(direction.origin, direction.direction.normalized * (target.transform.position - direction.origin).magnitude, Color.red, 10);
        GameObject mask = Instantiate(spawnType, target.transform.position - direction.direction.normalized * distanceFromObject, Quaternion.Euler(direction.direction));
        MaskController m = mask.GetComponent<MaskController>();
        m?.SetTarget(target);
        return mask;
    }
}
