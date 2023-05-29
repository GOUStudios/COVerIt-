using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlateHandsUp : MonoBehaviour
{
 
    public string levelsPlateTag = "LevelsPlate";
    public float rotationSpeed = 5f;

    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(levelsPlateTag))
            {
                Debug.Log("hitted" + hit.collider.name);
                RotateObjectTowardsCamera();
            }
            else
            {
                ResetObjectRotation();
            }
        }
        else
        {
            ResetObjectRotation();
        }
    }

    private void RotateObjectTowardsCamera()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        

        // Calcola la rotazione sull'asse Y locale
        Quaternion localRotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

        // Applica la rotazione sull'oggetto
        transform.rotation = Quaternion.Lerp(transform.rotation, localRotation, rotationSpeed * Time.deltaTime);
    }

    private void ResetObjectRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
    }
}
