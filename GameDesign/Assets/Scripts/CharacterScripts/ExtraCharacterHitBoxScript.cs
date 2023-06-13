using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExtraCharacterHitBoxScript : MonoBehaviour, Clickable
{

    [SerializeField] CustomerMonoBehavior parent;
    private Collider _collider;
    void Awake()
    {

        if (parent == null)
        {
            _collider = GetComponent<Collider>();
            if (_collider != null)
            {
                Debug.LogWarning("");
            }
            else return;
        }
    }

    public void Click(ClickType clickType)
    {
        
        parent.Click(clickType);
    }
}
