using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseKeyboardController : MonoBehaviour
{
    [SerializeField] UnityEvent m_FirstTimeAction;
    [SerializeField] UnityEvent m_SecondTimeAction;
    [SerializeField] KeyCode m_Key;

    private bool FirstTimeClick = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_Key))
        {
            if (FirstTimeClick) m_FirstTimeAction?.Invoke();
            else m_SecondTimeAction?.Invoke();

            FirstTimeClick = !FirstTimeClick;
        }
    }
}
