using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private int _numClicks;
    [SerializeField] private Camera _camera;
    [SerializeField] private MaskSpawner spawner;

    private MeshRenderer _rendered;
    private int curClicks = 0;

    private void Start()
    {
        _rendered = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        curClicks += 1;
        if(curClicks == _numClicks)
        {
            curClicks = 0;
            Debug.Log($"Clicked! {gameObject.name}");
            if (spawner != null)
                spawner.SpawnMask(_camera.ScreenPointToRay(Input.mousePosition), gameObject);

            _rendered.material.color = Color.red;
            
        }
    }
}
