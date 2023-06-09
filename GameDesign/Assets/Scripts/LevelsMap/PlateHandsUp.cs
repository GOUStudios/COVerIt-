using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlateHandsUp : MonoBehaviour
{
 
    public string levelsPlateTag = "LevelsPlate";
    public float rotationSpeed = 5f;

    public Vector3 specifiedRotation;

    private Quaternion initialRotation;

    private ChangeSceneRequest requestManager;
    //Name of the Scene to load
    public string sceneName;

    private void Start()
    {
        initialRotation = transform.rotation;

        requestManager = GetComponent<ChangeSceneRequest>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == transform.gameObject)
            {
                Debug.Log("hitted" + hit.collider.name);
                RotateObjectTowardsCamera();

                if (Input.GetMouseButtonDown(0)) requestManager.Request(sceneName);
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
        Quaternion targetRotation = Quaternion.Euler(specifiedRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ResetObjectRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
    }
}
