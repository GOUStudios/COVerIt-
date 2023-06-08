using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NPCMovementManager))]
public class CustomerMonoBehavior : MonoBehaviour, Clickable
{
    [SerializeField] public int id;
    [SerializeField] public float baseSpeed;
    [SerializeField] public float currentSpeed;
    [SerializeField] public int clickCunt = 0;
    [SerializeField] public int requiredClicks = 1;
    [SerializeField] public int clickTime;
    [SerializeField] public bool wearsMask;
    [SerializeField] public int pointValue;
    [SerializeField] public bool isFrozen = false;
    [SerializeField] public int frozenTime = 0;
    [SerializeField] public TaserManager taserManager = TaserManager.Instance;
    [SerializeField] private FiniteStateMachine<CustomerMonoBehavior> fsm;
    [SerializeField] private NPCMovementManager movementManager;
    [SerializeField] private Animator animator;
    private bool onGoingAnimation = false;
    private GameObject _mask;
    public string defaultLayer { get { return "Default"; } }

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

        State unmasked = new Unmasked("Unmasked", this);
        State masked = new Masked("Masked", this);
        State frozen = new Frozen("Frozen", this, animator);

        fsm.AddTransition(unmasked, masked, () => wearsMask);
        fsm.AddTransition(unmasked, frozen, () => isFrozen);
        fsm.AddTransition(masked, frozen, () => isFrozen);
        fsm.AddTransition(frozen, unmasked, () => !isFrozen && !wearsMask);
        fsm.AddTransition(frozen, masked, () => !isFrozen && wearsMask);

        if (wearsMask)
        {
            fsm.SetState(masked);
        }
        else
        {
            fsm.SetState(unmasked);
        }

        changeSpeed();
    }

    void Update()
    {
        fsm.Tik();
    }

    public void Click(ClickType clickType)
    {
        Debug.Log("RIGHT CLICK");
        if (clickType == ClickType.LEFT_CLICK)
        {
            clickCunt++;
            if (!wearsMask && clickCunt >= requiredClicks)
            {
                wearsMask = true;
                PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
            }
            else if (wearsMask)
            {
                StartCoroutine(DoTriggerAnimation("SmallHit"));
                PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
            }
            else
            {
                //Do here whatever feed back for click that are not the masking ones
            }
        }
        if (clickType == ClickType.RIGHT_CLICK)
        {
            Debug.Log("LEFT CLICK");
            if (taserManager.useTaser())
            {
                StartCoroutine(StartFreeze(frozenTime));
            }
        }

    }

    private IEnumerator StartFreeze(float duration)
    {
        if (!isFrozen)
        {
            isFrozen = true;
            yield return new WaitForSeconds(duration);
            isFrozen = false;
            Debug.Log("Unfreezed");
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
        if (newSpeed == 0)
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
    public void maskNPC()
    {
        _mask.SetActive(true);


        //TODO determine from which side. -> probably has to be done by the clicking , manager. -> for now default hit is set
        StartCoroutine(DoTriggerAnimation("GotHit"));
    }
    private IEnumerator DoTriggerAnimation(string name)
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

        _mask.SetActive(false);
    }


    private class Unmasked : State
    {
        private CustomerMonoBehavior _cmb;
        public Unmasked(string name, CustomerMonoBehavior cmb) : base(name)
        {
            _cmb = cmb;
        }

        public override void Enter()
        {
            _cmb.unmaskNPC();
        }

        public override void Tik() { }

        public override void Exit()
        {

        }

    }

    private class Masked : State
    {
        private CustomerMonoBehavior _cmb;
        public Masked(string name, CustomerMonoBehavior cmb) : base(name)
        {
            _cmb = cmb;
        }

        public override void Enter()
        {
            _cmb.maskNPC();
        }

        public override void Tik() { }

        public override void Exit() { }

    }

    private class Frozen : State
    {
        private CustomerMonoBehavior _cmb;
        private Animator _animator;
        public Frozen(string name, CustomerMonoBehavior cmb, Animator animator) : base(name)
        {
            _cmb = cmb;
            _animator = animator;
        }

        public override void Enter()
        {
            _cmb.changeLayer("ElectricityLayer");
            _cmb.changeSpeed(0f);
            _animator.SetBool("Tazered", true);
            Debug.Log("Enter frozen");
        }

        public override void Tik() { }

        public override void Exit()
        {

            _cmb.changeLayer(_cmb.defaultLayer);
            _cmb.changeSpeed();
            _animator.SetBool("Tazered", false);
        }

    }
}
