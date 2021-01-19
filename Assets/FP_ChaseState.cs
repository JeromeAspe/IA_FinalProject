using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_ChaseState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
           
            _brain.Movement.SetMoveTarget(_brain.Chase.GetSearchPoint());
            _brain.Movement.SetStateNav(true);
        };
        OnUpdate += () =>
        {
            _brain.Movement.MoveTo();
        };
    }
}
