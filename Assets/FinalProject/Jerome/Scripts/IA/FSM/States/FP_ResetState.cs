using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_ResetState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
           
            _brain.FSM.SetBool(_brain.PatrolParameter, true);
            _brain.FSM.SetBool(_brain.WaitParameter, false);
            _brain.FSM.SetBool(_brain.ChaseParameter, false);
            _brain.FSM.SetBool(_brain.AttackParameter, false);
            _brain.FSM.SetBool(_brain.ResetParameter, false);
            _brain.FSM.SetBool(_brain.DieParameter, false);
        };

        
        
    }
}
