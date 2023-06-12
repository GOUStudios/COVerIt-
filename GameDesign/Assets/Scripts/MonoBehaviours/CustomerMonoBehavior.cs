using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NPCMovementManager))]
public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] public int id;
    [SerializeField] public float baseSpeed;
    [ReadOnly][SerializeField] public float currentSpeed;
    [ReadOnly][SerializeField] public int clickCunt = 0;
    [SerializeField] public int requiredClicks = 1;
    [SerializeField] public int clickTime;
    [SerializeField] public bool wearsMask;
    [SerializeField] public int pointValue;
    [ReadOnly][SerializeField] public bool isFrozen = false;
    [SerializeField] public int frozenTime = 0;
    private TaserManager taserManager = TaserManager.Instance;
    private FiniteStateMachine<CustomerMonoBehavior> fsm;
    [SerializeField] private NPCMovementManager movementManager;
    [SerializeField] private Animator animator;
    protected bool onGoingAnimation = false;
    private GameObject _mask;
    public string defaultLayer { get { return "Default"; } }

    public AudioSource audioSource;

    public AudioClip shot;
    public AudioClip taser;
    public AudioClip missHit;
    public AudioClip cough;

    void Start()
    {

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null) Debug.LogError($"Missing Animator component: {name}");
        }
        if (movementManager == null)
        {
            movementManager = GetComponent<NPCMovementManager>();
            if (movementManager == null) Debug.LogError($"Missing MovementManager component: {name}");
        }
        _mask = transform.Find("Mask").gameObject;
        if (_mask == null) { Debug.LogError($"Error finding Mask of: {name}"); }

        fsm = new FiniteStateMachine<CustomerMonoBehavior>(this);

        State frozen = new Frozen("Frozen", this, animator);
        State moving = new MovingState("Moving", this);
        //TODO if a new behaviour is to be implemented do it here
        //(example, wait in queue. )

        fsm.AddTransition(frozen, moving, () => !isFrozen);
        fsm.AddTransition(moving, frozen, () => isFrozen);


        fsm.SetState(moving);

        maskNPC(wearsMask);

        changeSpeed();


        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        fsm.Tik();
    }

    public void Click(ClickType clickType)
    {
        onGoingAnimation = false;
        if (clickType == ClickType.LEFT_CLICK)
        {
            clickCunt++;
            if (!wearsMask && clickCunt >= requiredClicks)
            {
                wearsMask = true;
                PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
                maskNPC();
                audioSource.PlayOneShot(shot, 0.7f);
            }
            else if (wearsMask)
            {
                StartCoroutine(DoTriggerAnimation("SmallHit"));
                audioSource.PlayOneShot(missHit, 0.7f);
                PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
            }
            else
            {
                //TODO Do here whatever feed back for click that are not the masking ones (like dinosaurs)
                //maybe play a sound
            }
        }
        if (clickType == ClickType.RIGHT_CLICK)
        {
            if (taserManager.useTaser())
            {
                Debug.Log("freezing");
                StartCoroutine(StartFreeze(frozenTime));
            }
        }

    }

    private IEnumerator StartFreeze(float duration)
    {

        if (!isFrozen){
        
            isFrozen = true;
            audioSource.PlayOneShot(taser, 0.7f);   
            yield return new WaitForSeconds(duration);
            isFrozen = false;
        }
    }

    public void changeSpeed()
    {
        changeSpeed(baseSpeed);
    }
    public void changeSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
        animator.SetFloat("Speed", newSpeed);
        if (newSpeed <= 0.01)
        {
            movementManager.agent.isStopped = true;
        }
        else
        {
            movementManager.agent.isStopped = false;
            movementManager.agent.speed = newSpeed;
        }

    }
    public void changeLayer(string LayerName)
    {

        int Layer = LayerMask.NameToLayer(LayerName);
        if (Layer < 0)
        {
            Debug.LogWarning($"Could not find the expected layer {LayerName}");
            Layer = LayerMask.NameToLayer(defaultLayer);
        }

        gameObject.layer = Layer;
        foreach (Transform child in transform)
        {
            child.gameObject.layer = Layer;
        }


    }
    public void maskNPC(bool value)
    {
        if (value) maskNPC(); else unmaskNPC();
    }
    public void maskNPC()
    {
        _mask.SetActive(true);
        //TODO determine from which side. -> probably has to be done by the clicking , manager. -> for now default hit is set
        StartCoroutine(DoTriggerAnimation("GotHit"));
    }
    protected IEnumerator DoTriggerAnimation(string name)
    {
        onGoingAnimation = true;
        changeSpeed(0);
        animator.SetTrigger(name);
        yield return new WaitWhile(() => onGoingAnimation);
        changeSpeed();
    }
    public void animationFinished()
    {
        onGoingAnimation = false;
    }
    private void unmaskNPC()
    {
        _mask.SetActive(false);
    }

}
