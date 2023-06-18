using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TutorialScriptableObject : ScriptableObject
{
    [TextAreaAttribute] public string ExplanationText = "DefaultTrialText";

    public TimedEvent[] tutorialEventList;
    public Func<bool> tutorialActivationCondition;

    public virtual void tutorialProcedure() { 
        
    }

}
