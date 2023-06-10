using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnCorrectlyClicked;
    public static event ClickAction OnMissClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //The second condition checks if the pointer is clicking on an UI element
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Clickable clickable = (Clickable)ObjectUtils.GetObjectWithInterface<Clickable>(hit.collider.gameObject);
                if(clickable != null)
                {
                    clickable.Click(ClickType.LEFT_CLICK);
                    OnCorrectlyClicked?.Invoke();
                }
                else
                {
                    OnMissClicked?.Invoke();
                }
                
            }
            else
            {
                OnMissClicked?.Invoke();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Clickable clickable = (Clickable)ObjectUtils.GetObjectWithInterface<Clickable>(hit.collider.gameObject);
                if(clickable != null)
                {
                    clickable.Click(ClickType.RIGHT_CLICK);
                }
            }
        }
    }
}
