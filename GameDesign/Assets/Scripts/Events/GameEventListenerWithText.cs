using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameEventListenerWithText : GameEventListener
{
    [TextAreaAttribute]
    [SerializeField] string DescriptionToSet;
    [SerializeField] TextMeshProUGUI TextMeshProUGUI;
    [SerializeField] bool isUsingTutorialCanvas = true;
    [SerializeField] TutorialCanvasHandler canvasHandler;

    [ReadOnly]public bool hasBeenCalled = false;

    public AudioSource bossMutter;

    void Awake()
    {
        if (TextMeshProUGUI == null) Debug.LogError("Missing Reference to TextMeshComponent");
    }

    public override void OnEventRaised()
    {
        if (canvasHandler != null && isUsingTutorialCanvas)
            StartCoroutine(WaitForTutorialCanvas());
        else raiseEvent();
    }
    private void raiseEvent()
    {
        TextMeshProUGUI.text = DescriptionToSet;
        base.OnEventRaised();
    }
    IEnumerator WaitForTutorialCanvas()
    {
        hasBeenCalled = false;
        canvasHandler.addEventToQueue(this);
        yield return new WaitUntil(() => hasBeenCalled);
        bossMutter = GetComponent<AudioSource>();
        bossMutter?.Play();
        raiseEvent();
    }
}
