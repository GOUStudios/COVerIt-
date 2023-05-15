using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserController : MonoBehaviour
{

    private MeshRenderer _rendered;
    private Color originalColor;

    // Start is called before the first frame update
    private void Start()
    
    {
        _rendered = GetComponent<MeshRenderer>();
        originalColor = Color.blue;
        _rendered.material.color = originalColor;
    }

    
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log($"Left Clicked! {Input.GetMouseButton(0)}");
            _rendered.material.color = Color.red;
        }else if(Input.GetMouseButton(1)){
            Debug.Log($"Right Clicked! {Input.GetMouseButton(1)}");
            _rendered.material.color = Color.yellow;
        }else
        {
            _rendered.material.color = originalColor;
        }
    }

    private void OnMouseExit(){
        
    }
}
