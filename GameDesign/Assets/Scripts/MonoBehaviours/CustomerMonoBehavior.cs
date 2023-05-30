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

    void Start()
    {
        InvokeRepeating("UnFreeze", 1.0f, 1.0f);
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
    }

    void Update() {
        fsm.Tik();    
    }

    public void Click(ClickType clickType)
    {
        
        if(clickType == ClickType.LEFT_CLICK)
        {
            clickCunt++;
            if (!wearsMask  && clickCunt >= requiredClicks)
            {
                wearsMask = true;
                PointsManager.Instance.TriggerEvent_IncrementPoints(pointValue);
            }
            else
            {
                PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * pointValue);
            }
        }
        if(clickType == ClickType.RIGHT_CLICK)
        {
            if(taserManager.useTaser()){
                if(!isFrozen)
                {
                    isFrozen = true;
                    currentSpeed = 0;
                    frozenTimeCount = 0;
                }
            }
        }
        
    }

    private void UnFreeze()
    {
        if (isFrozen){
            if(frozenTimeCount == 3)
            {
                isFrozen = false;
                currentSpeed = baseSpeed;
                frozenTimeCount = 0;
            }else
            {
                frozenTimeCount++;
            }
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
