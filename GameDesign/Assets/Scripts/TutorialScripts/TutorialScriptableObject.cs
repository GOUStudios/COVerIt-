using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class TutorialScriptableObject : ScriptableObject
{
    [TextAreaAttribute] public string ExplanationText = "DefaultTrialText";

    
    public Func<bool> tutorialActivationCondition;

    public abstract void tutorialProcedure();

}
