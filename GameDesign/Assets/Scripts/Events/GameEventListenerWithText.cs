using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameEventListenerWithText : GameEventListener
{
    [TextAreaAttribute]
    [SerializeField] string DescriptionToSet;
    [SerializeField] TextMeshProUGUI TextMeshProUGUI;

    void Awake()
    {
        if (TextMeshProUGUI == null) Debug.LogError("Missing Reference to TextMeshComponent");
    }

    public override void OnEventRaised()
    {
        TextMeshProUGUI.text = DescriptionToSet;
        base.OnEventRaised();
    }
}
