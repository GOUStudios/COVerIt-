using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NPCMovementManager))]
[RequireComponent(typeof(AudioSource))]
public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] public int id;
    [ReadOnly][SerializeField] private string currentState;
    [SerializeField] public float baseSpeed;
    [ReadOnly][SerializeField] public float currentSpeed;
    [SerializeField] public float maxTimeToReachWaypoint = 15f;
    [ReadOnly][SerializeField] private int clickCunt = 0;
    [SerializeField] public int requiredClicks = 1;
    [SerializeField] public float clickTime;
    [SerializeField] public bool wearsMask;
    [SerializeField] public int pointValue;
    [ReadOnly][SerializeField] public bool isFrozen = false;
    [SerializeField] public float frozenTime = 0f;
    private TaserManager taserManager = TaserManager.Instance;
    protected FiniteStateMachine<CustomerMonoBehavior> fsm;
    [SerializeField] protected NPCMovementManager movementManager;
    [SerializeField] protected Animator animator;
    public bool onGoingAnimation { get; private set; }
    [SerializeField] private GameObject _mask;
    public string defaultLayer = "Default";

    public AudioSource audioSource;

    public AudioClip shot;
    public AudioClip taser;
    public AudioClip missHit;
    public AudioClip cough;
    public AudioClip HitButNotMasked;

    void Awake()
    {
        clickCunt = 0;
        onGoingAnimation = false;

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
        if (_mask == null) _mask = transform.Find("Mask").gameObject;
        if (_mask == null) { Debug.LogError($"Error finding Mask of: {name}"); }
        fsm = new FiniteStateMachine<CustomerMonoBehavior>(this);
        movementManager.MaxTimeToReachTarget = maxTimeToReachWaypoint;

        if (wearsMask) maskNPC(false); else unmaskNPC();

        changeSpeed();
        audioSource = GetComponent<AudioSource>();

        //ideal if setting the FSM is the last function. just to make sure the other parameters are set if they are to be changed by the states.

        setFSM();

    }

    void Update()
    {
        currentState = fsm._currentState.Name;
        fsm.Tik();
    }

    #region Changing Methods
    protected virtual void setFSM()
    {

        State frozen = new Frozen("Frozen", this, animator);
        State moving = new MovingState("Moving", this, movementManager);

        fsm.AddTransition(frozen, moving, () => !isFrozen);
        fsm.AddTransition(moving, frozen, () => isFrozen);

        fsm.SetState(moving);
    }
    protected virtual void onHitBehaviour()
    {
        wearsMask = true;

        ClickManager.Instance.onCorrectlyClickInvoke();
        audioSource.PlayOneShot(shot, 0.7f);
        PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
        StartCoroutine(HitCoroutine());
        maskNPC();
    }
    protected virtual void onFreezeBehaviour()
    {
        StartCoroutine(StartFreeze(frozenTime));
    }
    protected virtual void DodgeHitBehaviour()
    {
        audioSource.PlayOneShot(missHit, 0.7f);
        PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
        StartCoroutine(wrongHitCoroutine());
        StartCoroutine(DoTriggerAnimation("SmallHit"));

    }

    #endregion

    public void Click(ClickType clickType)
    {
        onGoingAnimation = false;
        if (clickType == ClickType.LEFT_CLICK)
        {
            clickCunt++;
            if (!wearsMask && clickCunt >= requiredClicks)
            {

                onHitBehaviour();
            }
            else if (wearsMask)
            {
                ClickManager.Instance.onMissClickInvoke();
                DodgeHitBehaviour();

            }
            else
            {
                audioSource.PlayOneShot(HitButNotMasked, 0.7f);
                StartCoroutine(HitCoroutine());
            }
        }
        if (clickType == ClickType.RIGHT_CLICK)
        {
            if (taserManager.useTaser())
            {
                onFreezeBehaviour();
            }
        }

    }
    private IEnumerator wrongHitCoroutine()
    {
        VFXManager.Instance.changeLayer(gameObject, "WrongHitLayer");
        audioSource.PlayOneShot(HitButNotMasked, 0.7f);
        yield return new WaitForSeconds(0.3f);
        VFXManager.Instance.changeLayer(gameObject, defaultLayer);
    }
    private IEnumerator HitCoroutine()
    {
        VFXManager.Instance.changeLayer(gameObject, "HitLayer");
        audioSource.PlayOneShot(HitButNotMasked, 0.7f);
        yield return new WaitForSeconds(0.3f);
        VFXManager.Instance.changeLayer(gameObject, defaultLayer);
    }
    private IEnumerator StartFreeze(float duration)
    {

        if (!isFrozen)
        {

            isFrozen = true;
            audioSource.loop = true;
            audioSource.clip = taser;
            audioSource.volume = 0.7f;
            audioSource.Play();
            yield return new WaitForSeconds(duration);
            audioSource.Stop();
            audioSource.volume = 1f;
            audioSource.loop = false;
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

    public void maskNPC(bool playAnimation)
    {
        wearsMask = true;
        _mask.SetActive(true);
        tag = "Masked";
        //TODO determine from which side. -> probably has to be done by the clicking , manager. -> for now default hit is set
        if (playAnimation)
            StartCoroutine(DoTriggerAnimation("GotHit"));
    }
    public void maskNPC()
    {
        maskNPC(true);
    }



    public void doTriggerAnimation(string name)
    {
        StartCoroutine(DoTriggerAnimation(name));
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
    public void unmaskNPC()
    {
        tag = "Untagged";
        wearsMask = false;
        clickCunt = 0;
        _mask.SetActive(false);
    }

}
