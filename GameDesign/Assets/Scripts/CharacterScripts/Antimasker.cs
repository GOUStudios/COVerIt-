using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antimasker : CustomerMonoBehavior
{
    CustomerMonoBehavior targetToUnmask;
    bool hasBeenCaught = false;
    protected override void setFSM()
    {
        State frozen = new Frozen("Frozen", this, animator);
        State unmasking = new Unmasking("Unmasking");
        State moving = new MovingState("Moving", this);

        fsm.AddTransition(frozen, moving, () => !isFrozen && hasBeenCaught);
        fsm.AddTransition(moving, frozen, () => isFrozen);
        fsm.AddTransition(unmasking, frozen, () => isFrozen);
        fsm.AddTransition(frozen, unmasking, () => !isFrozen && !hasBeenCaught);
        fsm.AddTransition(unmasking, moving, () => hasBeenCaught);

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
            DodgeHitBehaviour();
        }

    }

    protected override void onFreezeBehaviour()
    {
        hasBeenCaught = true;
        base.onFreezeBehaviour();

    }
}
