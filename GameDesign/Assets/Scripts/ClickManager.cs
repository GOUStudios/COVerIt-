using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private BossAngerManager bossAngerManager;
    // Start is called before the first frame update
    void Start()
    {
        bossAngerManager = BossAngerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                bossAngerManager.triggerEvent_CorrectlyClicked();
            }
            else
            {
                bossAngerManager.triggerEvent_MissClicked();
            }
        }
    }
}
