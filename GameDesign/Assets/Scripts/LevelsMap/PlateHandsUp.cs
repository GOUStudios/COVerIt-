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

    private AudioSource clickSound;

    private ChangeSceneRequest requestManager;
    //Name of the Scene to load
    public string sceneName;

    private void Start()
    {
        initialRotation = transform.rotation;

        requestManager = GetComponent<ChangeSceneRequest>();

        clickSound= GetComponent<AudioSource>();
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

                CheckClick(hit);
                    
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

    private void CheckClick(RaycastHit hit)
    {
        if (Input.GetMouseButtonDown(0) &&
                    hit.collider.gameObject.CompareTag("LevelsPlate"))
        {
            if(clickSound!= null) clickSound.Play();

            requestManager.RequestLevel(sceneName);
            Debug.Log("Plates Clicked " + hit.collider.name);
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
