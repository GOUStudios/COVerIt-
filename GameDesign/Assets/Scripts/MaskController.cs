using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [ReadOnly][SerializeField]private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            var direction = _target.transform.position - transform.position;
            transform.position += speed * Time.deltaTime * direction.normalized;
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }
}
