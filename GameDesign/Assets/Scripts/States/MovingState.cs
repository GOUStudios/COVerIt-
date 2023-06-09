using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    private CustomerMonoBehavior _cmb;
    private NPCMovementManager _moveManager;
    public MovingState(string name, CustomerMonoBehavior cmb, NPCMovementManager movementManager) : base(name)
    {
        _cmb = cmb;
        _moveManager = movementManager;
    }
    public override void Enter()
    {
        _cmb.defaultLayer = "Default";
        VFXManager.Instance.changeLayer(_cmb.gameObject, _cmb.defaultLayer);
    }

    public override void Exit()
    {
    }

    public override void Tik()
    {
        _moveManager.goingToWaypointTik();
    }
}
