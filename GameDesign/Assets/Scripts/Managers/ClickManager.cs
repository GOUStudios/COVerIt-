using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(AudioSource))]
public class ClickManager : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction OnCorrectlyClicked;
    public static event ClickAction OnMissClicked;
    private bool isPaused = true;

    [SerializeField] private AudioSource audioSource;
    public AudioClip miss;

    private static ClickManager _instance;
    public static ClickManager Instance
    {
        get
        {
            return _instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else if (_instance == null) _instance = this;
        else { Debug.LogWarning("Could not create instance of ClickManager"); }
        TimerManagerMonoBehaviour.OnTimePause += TimePauseBehavior;
        TimerManagerMonoBehaviour.OnTimeResume += TimeResumeBehavior;
        TimerManagerMonoBehaviour.OnTimeStart += TimeResumeBehavior;
    }

    void OnDestroy()
    {
        TimerManagerMonoBehaviour.OnTimePause -= TimePauseBehavior;
        TimerManagerMonoBehaviour.OnTimeResume -= TimeResumeBehavior;
        TimerManagerMonoBehaviour.OnTimeStart -= TimeResumeBehavior;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            //The second condition checks if the pointer is clicking on an UI element
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {

                    VFXManager.Instance.spawnSmokeAt(hit.transform, hit.point, Quaternion.LookRotation(hit.normal));


                    Clickable clickable = (Clickable)ObjectUtils.GetObjectWithInterface<Clickable>(hit.collider.gameObject);
                    if (clickable != null)
                    {

                        clickable.Click(ClickType.LEFT_CLICK);
                        //OnCorrectlyClicked?.Invoke(); now invoked from the character
                    }
                    else
                    {
                        audioSource.PlayOneShot(miss, 0.7f);
                        OnMissClicked?.Invoke();
                    }

                }
                else
                {
                    audioSource.PlayOneShot(miss, 0.7f);
                    OnMissClicked?.Invoke();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Clicked with RIGHT click");
                    Clickable clickable = (Clickable)ObjectUtils.GetObjectWithInterface<Clickable>(hit.collider.gameObject);
                    Debug.Log("Clickable : " + clickable);
                    if (clickable != null)
                    {
                        Debug.Log("Clicked with RIGHT click");
                        clickable.Click(ClickType.RIGHT_CLICK);
                    }
                }
            }
        }
    }

    public void onCorrectlyClickInvoke()
    {
        OnCorrectlyClicked?.Invoke();
    }
    public void onMissClickInvoke()
    {
        OnMissClicked?.Invoke();
    }

    void TimePauseBehavior()
    {
        isPaused = true;
    }

    void TimeResumeBehavior()
    {
        isPaused = false;
    }
}
