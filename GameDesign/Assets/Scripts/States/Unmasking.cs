using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unmasking : State
{
    Antimasker _cmb;
    NPCMovementManager movementManager;
    CustomerMonoBehavior target;
    Animator _animator;
    public Unmasking(string name, Antimasker cmb, NPCMovementManager nPCMovementManager, Animator animator) : base(name)
    {
        _cmb = cmb;
        movementManager = nPCMovementManager;
        _animator = animator;
    }

    public override void Enter()
    {
        movementManager.MaxTimeToReachTarget = _cmb.timeToCatchTarget;
        movementManager.goToTargetToUnmask();// first call tries to find a target
    }

    public override void Exit()
    {
        movementManager.targetCustomer = null;
        movementManager.MaxTimeToReachTarget = _cmb.maxTimeToReachWaypoint;
    }

    public override void Tik()
    {
        if (movementManager.goToTargetToUnmask())
        {
            PointsManager.Instance.TriggerEvent_IncrementPoints(-1 * _cmb.pointValue);
            //play animation to unmask.
            _cmb.doTriggerAnimation("Unmask");
        }

    }

}
