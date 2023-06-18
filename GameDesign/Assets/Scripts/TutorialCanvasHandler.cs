using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialCanvasHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PopUpCanvas()
    {
        animator.SetBool("isShown", true);
    }
    public void HideCanvas()
    {
        animator.SetBool("isShown", false);
    }
}
