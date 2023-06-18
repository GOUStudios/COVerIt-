using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Tutorial", menuName = "Tutorial/Game tutorial", order = 1)]

public class TutorialScriptableObject : ScriptableObject
{
    [TextAreaAttribute] public string ExplanationText = "DefaultTrialText";

    public TimedEvent[] tutorialEventList;
    public Func<bool> tutorialActivationCondition;
    public bool isOnGoing{ get; private set; }

    public virtual IEnumerator tutorialProcedure()
    {
        isOnGoing = true;
        for (int i = 0; i < tutorialEventList.Length; i++)
        {
            tutorialEventList[i].Raise();
            if (tutorialEventList[i].Duration > 0)
                yield return new WaitForSeconds(tutorialEventList[i].Duration);
        }
        isOnGoing = false;
    }

}
