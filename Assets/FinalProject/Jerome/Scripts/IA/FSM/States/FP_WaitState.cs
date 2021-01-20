using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_WaitState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        
        base.InitState(_brain);

        OnEnter += () =>
        {
            _brain.Movement.SetMoveTarget(_brain.transform.position);
            _brain.Movement.SetStateNav(false);
        };
        OnExit += () =>
        {
            
            _brain.FSM.SetBool(_brain.WaitParameter, false);
        };
    }
}
