using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseKeyboardController : MonoBehaviour
{
    [SerializeField] UnityEvent m_FirstTimeAction;
    [SerializeField] UnityEvent m_SecondTimeAction;
    [SerializeField] KeyCode m_Key;

    Coroutine ongoingTask=null;
    private bool FirstTimeClick = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_Key))
        {
            Debug.Log(m_Key +" was pressed");
            if (FirstTimeClick && TimerManagerMonoBehaviour.IsRunning && ongoingTask==null)
            {
                ongoingTask=StartCoroutine(OnFirstClickCoroutine());
            }

            else if (!FirstTimeClick && !TimerManagerMonoBehaviour.IsRunning && ongoingTask==null)
            {
                ongoingTask=StartCoroutine(OnSecondClick());
            }
        }
    }
    public void changeState()
    {
        FirstTimeClick = !FirstTimeClick;
    }
    IEnumerator OnFirstClickCoroutine()
    {
        Debug.Log("pausing");
        m_FirstTimeAction?.Invoke();
        FirstTimeClick = !FirstTimeClick;
        yield return new WaitForSecondsRealtime(0.5f);
        ongoingTask=null;
    }
    IEnumerator OnSecondClick()
    {
        Debug.Log("unpausing");
        m_SecondTimeAction?.Invoke();
        FirstTimeClick = !FirstTimeClick;
        yield return new WaitForSecondsRealtime(0.5f);
        ongoingTask=null;
    }
}
