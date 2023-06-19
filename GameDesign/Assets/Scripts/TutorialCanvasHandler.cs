using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialCanvasHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    private List<GameEventListenerWithText> Listeners = new List<GameEventListenerWithText>();

    Coroutine tryingToPopOut;
    public static bool isOut { get; private set; }

    void Awake()
    {
        isOut = false;
    }
    public void PopUpCanvas()
    {
        animator.SetBool("isShown", true);
    }

    public void addEventToQueue(GameEventListenerWithText _event)
    {
        if (!Listeners.Contains(_event))
            Listeners.Add(_event);
        if (tryingToPopOut == null)
        {
            tryingToPopOut = StartCoroutine(DeQueueEvent());
        }
    }

    IEnumerator DeQueueEvent()
    {
        while (Listeners.Count > 0)
        {
            isOut = true;
            GameEventListenerWithText GE = Listeners[0];
            Listeners.RemoveAt(0);

            GE.hasBeenCalled = true;

            yield return new WaitWhile(() => isOut);
        }
        if (Listeners.Count <= 0)
        {
            tryingToPopOut = null;
            yield break;
        }
    }

    public void HideCanvas()
    {
        StartCoroutine(HidingCanvas());
    }
    IEnumerator HidingCanvas()
    {
        animator.SetBool("isShown", false);
        yield return new WaitForSeconds(0.5f);
        isOut = false;
    }
}
