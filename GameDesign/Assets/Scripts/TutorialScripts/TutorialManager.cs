using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] string text = "Default text.";
    [SerializeField] bool UIIsShown = false;
    [SerializeField] Animator animator;
    [SerializeField] TutorialScriptableObject[] levelTutorials;





    void Awake()
    {
        if (TextMeshProUGUI == null) Debug.LogError($"{name} is missing its TextMesh component");
        if (animator == null) Debug.LogError($"{name} is missing its Animator component");
        if (levelTutorials.Length <= 0)
        {
            Debug.LogWarning("There were no tutorials set for the level");
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Character {other.name} entered the scene");
        StartCoroutine(CheckTutorials());
    }


    private IEnumerator CheckTutorials()
    {

        foreach (TutorialScriptableObject tutorial in levelTutorials)
        {
            if (tutorial.tutorialActivationCondition())
            {
                StartCoroutine(tutorial.tutorialProcedure());
                yield return new WaitUntil(() => !tutorial.isOnGoing);
            }
        }
    }
    public void doTutorial()
    {

        changeText(text);
        popUp(true);

    }
    /**
    *will do a coroutine to do a pop up of the tutorial
    */
    public void popUp(bool newState)
    {
        animator.SetBool("isShown", newState);
    }

    public void changeText(string newText)
    {
        TextMeshProUGUI.text = newText;
    }
}
