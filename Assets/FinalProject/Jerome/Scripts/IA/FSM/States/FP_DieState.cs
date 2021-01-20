using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_DieState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
          {
              _brain.Animations.SetDieAnimation(true);
              _brain.Movement.SetStateNav(false);
              _brain.Movement.SetMoveTarget(_brain.transform.position);
              _brain.IsEnabled = false;
          };
        OnExit += () =>
        {
            Debug.Log("exit");
            _brain.Animations.SetDieAnimation(false);
            _brain.FSM.SetBool(_brain.DieParameter, false);
            _brain.ResetStates();
        };
    }

    
}
