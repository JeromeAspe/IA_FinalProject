using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_DieState : FP_State
{
    float timer = 0;
    bool canExit = false;
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
          {
              
              _brain.Movement.SetStateNav(false);
              _brain.Movement.SetMoveTarget(_brain.transform.position);
              _brain.IsEnabled = false;
              _brain.FSM.SetBool(_brain.PatrolParameter, false);
              _brain.FSM.SetBool(_brain.WaitParameter, false);
              _brain.FSM.SetBool(_brain.ChaseParameter, false);
              _brain.FSM.SetBool(_brain.AttackParameter, false);
              _brain.FSM.SetBool(_brain.ResetParameter, false);
              _brain.Animations.SetDieAnimation(true);
          };
        OnUpdate += () =>
        {
            if (canExit)
            {
                _brain.ResetStates();
            }
            Timer();
        };
        OnExit += () =>
        {
            _brain.Animations.SetDieAnimation(false);
            canExit = false;
            
        };
    }

    public void Timer()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            timer = 0;
            canExit = true;
            
        }
    }

    
}
