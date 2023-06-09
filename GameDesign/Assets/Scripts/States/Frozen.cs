using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frozen : State
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