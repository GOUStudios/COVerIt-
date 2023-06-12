using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    private CustomerMonoBehavior _cmb;
    public MovingState(string name, CustomerMonoBehavior cmb) : base(name) {
        _cmb = cmb;
    }
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Tik()
    {
    }
}
