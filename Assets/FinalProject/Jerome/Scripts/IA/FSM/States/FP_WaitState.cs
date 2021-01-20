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
            /* if(_brain.CoverBehaviour.HasTarget)
                 _brain.Movement.SetMoveTarget(_brain.CoverBehaviour.GetTarget()*-1);
             else
                 _brain.Movement.SetMoveTarget(_brain.transform.position);*/
            _brain.Animations.SetWaitAnimation(true);
            _brain.Movement.SetMoveTarget(_brain.transform.position - _brain.transform.forward);
            _brain.Movement.SetStateNav(false);
            
        };
        OnUpdate += () =>
        {
            _brain.Movement.RotateTo();
        };
        OnExit += () =>
        {
            
            _brain.FSM.SetBool(_brain.WaitParameter, false);
            _brain.Animations.SetWaitAnimation(false);
        };
    }

    
}
