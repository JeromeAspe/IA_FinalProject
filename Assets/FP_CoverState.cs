using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_CoverState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
            _brain.Movement.SetMoveTarget(_brain.CoverBehaviour.GetBestCover());
            _brain.Movement.SetStateNav(true);
            
        };
        OnUpdate += () =>
        {
            //_brain.Movement.SetMoveTarget(_brain.CoverBehaviour.GetBestCover());
            _brain.Movement.MoveTo();
        };
        OnExit += () =>
        {
            _brain.FSM.SetBool(_brain.CoverParameter, false);
        };
    }
}
