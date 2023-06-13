using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antimasker : CustomerMonoBehavior
{
    bool hasBeenCaught = false;
    [SerializeField]public float timeToCatchTarget = 0.75f;
    protected override void setFSM()
    {
        State frozen = new Frozen("Frozen", this, animator);
        State unmasking = new Unmasking("Unmasking", this, movementManager, animator);
        State moving = new MovingState("Moving", this, movementManager);

        fsm.AddTransition(frozen, moving, () => !isFrozen && hasBeenCaught);
        fsm.AddTransition(moving, frozen, () => isFrozen);
        fsm.AddTransition(unmasking, frozen, () => isFrozen);
        fsm.AddTransition(frozen, unmasking, () => !isFrozen && !hasBeenCaught);
        fsm.AddTransition(unmasking, moving, () => hasBeenCaught);
        fsm.AddTransition(moving, unmasking, () => !hasBeenCaught);

        fsm.SetState(unmasking);
    }

    protected override void onHitBehaviour()
    {
        if (hasBeenCaught)
        {
            base.onHitBehaviour();
        }
        else
        {
            ClickManager.Instance.onMissClickInvoke();
            DodgeHitBehaviour();
        }

    }

    protected override void onFreezeBehaviour()
    {
        hasBeenCaught = true;
        base.onFreezeBehaviour();
    }
}
