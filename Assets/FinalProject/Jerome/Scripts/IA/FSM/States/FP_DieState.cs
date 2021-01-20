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
              
          };
        OnExit += () =>
        {
            _brain.ResetStates();
        };
    }

    
}
