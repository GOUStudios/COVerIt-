using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager : MonoBehaviour{

    //Taser variables
    [SerializeField] private int taserBattery;
    [SerializeField] private int taserCost = 20;
    [SerializeField] private int taserMaxBattery = 100;
    [SerializeField] private bool trigerIsFree = true;
    [SerializeField] private int chargePerSecond = 2;

    private MeshRenderer _rendered;
    private Color originalColor;
    

    // Start is called before the first frame update
    private void Start(){
        _rendered = GetComponent<MeshRenderer>();
        originalColor = Color.blue;
        _rendered.material.color = originalColor;
        InvokeRepeating("ChargeBattery", 1.0f, 1.0f);
    }
    
    private void OnMouseOver(){
        if (Input.GetMouseButton(1) && trigerIsFree)
        {
            trigerIsFree = false;
            if (taserBattery>=taserCost )
            {
                taserBattery = taserBattery - taserCost;
                _rendered.material.color = Color.yellow;
            }
            Debug.Log($"Decreased battery to: {taserBattery}");
        }else
        {
            _rendered.material.color = originalColor;
        }
    }

    private void OnMouseExit(){
        trigerIsFree = true;
        _rendered.material.color = originalColor;
    }

    private void ChargeBattery(){
        if (taserBattery < taserMaxBattery)
        {
            taserBattery += chargePerSecond;
            Debug.Log($"Taser Battery = {taserBattery}");
        }
    }
}
