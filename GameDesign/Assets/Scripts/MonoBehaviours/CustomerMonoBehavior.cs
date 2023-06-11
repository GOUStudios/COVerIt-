using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] public int frozenTimeCount = 0;

    [SerializeField] public TaserManager taserManager = TaserManager.Instance;

    [SerializeField] private FiniteStateMachine<CustomerMonoBehavior> fsm;

    public AudioSource audioSource;

    public AudioClip shot;
    public AudioClip taser;
    public AudioClip missHit;
    public AudioClip cough;

    void Start()
    {
        currentSpeed = baseSpeed;

        fsm = new FiniteStateMachine<CustomerMonoBehavior>(this);

        State unmasked = new Unmasked("Unmasked", this);
        State masked = new Masked("Masked", this);
        State frozen = new Frozen("Frozen", this);

        fsm.AddTransition(unmasked, masked, () => wearsMask);
        fsm.AddTransition(unmasked, frozen, () => isFrozen);
        fsm.AddTransition(masked, frozen, () => isFrozen);
        fsm.AddTransition(frozen, unmasked, () => !isFrozen && !wearsMask);
        fsm.AddTransition(frozen, masked, () => !isFrozen && wearsMask);

        if(wearsMask){
            fsm.SetState(masked);
        }else{
            fsm.SetState(unmasked);
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        fsm.Tik();    
    }

    public void Click(ClickType clickType)
    {
        Debug.Log("RIGHT CLICK");
        if(clickType == ClickType.LEFT_CLICK)
        {
            clickCunt++;
            if (!wearsMask  && clickCunt >= requiredClicks)
            {
                wearsMask = true;
                PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
                audioSource.PlayOneShot(shot, 0.7f);
            }
            else
            {
                audioSource.PlayOneShot(missHit, 0.7f);
                PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
            }
        }
        if(clickType == ClickType.RIGHT_CLICK)
        {
            Debug.Log("LEFT CLICK");
            if(taserManager.useTaser()){
                StartCoroutine(StartFreeze(3f)); 
            }
        }
        
    }

    private IEnumerator StartFreeze(float duration)
    {
        if (!isFrozen){
            audioSource.PlayOneShot(taser, 0.7f);
			isFrozen = true;
            yield return new WaitForSeconds(duration);
            isFrozen = false;
            currentSpeed = baseSpeed;
            Debug.Log("Unfreezed");
        }
    }

    bool hasMask()
    {
        return true;
    }

    private class Unmasked: State{
        private CustomerMonoBehavior _cmb;
        public Unmasked(string name, CustomerMonoBehavior cmb) : base(name){
            _cmb = cmb;
        }

        public override void Enter(){
            Debug.Log("Enter unmasked");
        }

        public override void Tik(){}

        public override void Exit(){}

    }

    private class Masked: State{
        private CustomerMonoBehavior _cmb;
        public Masked(string name, CustomerMonoBehavior cmb) : base(name){
            _cmb = cmb;
        }

        public override void Enter(){
            Debug.Log("Enter masked");
        }

        public override void Tik(){}

        public override void Exit(){}

    }

    private class Frozen: State{
        private CustomerMonoBehavior _cmb;
        public Frozen(string name,CustomerMonoBehavior cmb) : base(name){
            _cmb = cmb;
        }

        public override void Enter(){
            Debug.Log("Enter frozen");
        }

        public override void Tik(){}

        public override void Exit(){}

    }
}
