using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_AttackState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
            _brain.Movement.SetStateNav(false);
        };
        OnUpdate += () =>
        {
            _brain.FightSystem.OnAttack?.Invoke();
            _brain.Movement.RotateTo();
        };
        OnExit += () =>
        {
            _brain.Movement.SetStateNav(true);
        };
    }
}
