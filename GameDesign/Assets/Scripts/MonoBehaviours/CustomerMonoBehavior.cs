using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] public int id;
    [SerializeField] public float baseSpeed;
    [SerializeField] public float currentSpeed;
    [SerializeField] public int clickAmount = 0;
    [SerializeField] public int clickTime;
    [SerializeField] public bool wearsMask;
    [SerializeField] public NPCMovementManager moveManager;
    [SerializeField] public int pointValue;
    [SerializeField] public bool isFrozen = false;
    [SerializeField] public int frozenTimeCount = 0;

    [SerializeField] public TaserManager taserManager = TaserManager.Instance;

    void Start()
    {
        // Llamo acá UnFreeze, para que el invocador no se detenga por el fin de otra fn
        InvokeRepeating("UnFreeze", 1.0f, 1.0f);
        currentSpeed = baseSpeed;
    }

    public void Click(ClickType clickType)
    {
        if(clickType == ClickType.LEFT_CLICK)
        {
            clickAmount++;
            if (!wearsMask)
            {
                wearsMask = true;
                PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
            }
            else
            {
                PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
            }
        }
        // EL CLICK NO ME ESTABA FUNCIONANDO, LO IMPLEMENTÉ A MI MANERA
        // SEBASTIAN COME VERGAS
    }

    private void UnFreeze()
    {
        if (isFrozen){
            if(frozenTimeCount == 3)
            {
                isFrozen = false;
                currentSpeed = baseSpeed;
                frozenTimeCount = 0;
                Debug.Log("Object is no longer stuned!");
            }else
            {
                frozenTimeCount++;
            }
        }
    }

    bool hasMask()
    {
        return true;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && taserManager.triggerIsFree)
        {
            taserManager.triggerIsFree = false;
            if (taserManager.taserBattery >= taserManager.taserCost)
            {
                taserManager.taserBattery = taserManager.taserBattery - taserManager.taserCost;
                if(!isFrozen)
                {
                    isFrozen = true;
                    currentSpeed = 0;
                    frozenTimeCount = 0; // Restart counter
                }
            }
            Debug.Log($"Decreased battery to: {taserManager.taserBattery}");
        }
    }

    private void OnMouseExit(){
        taserManager.triggerIsFree = true;
    }

}
