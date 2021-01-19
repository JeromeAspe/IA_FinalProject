using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_WaitState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnExit += () =>
        {
            _brain.FSM.SetBool(_brain.WaitParameter, false);
        };
    }
}
