using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PatrolState : FP_State
{
    public override void InitState(FP_IABrain _brain)
    {
        base.InitState(_brain);
        OnEnter += () =>
        {
            _brain.Movement.SetStateNav(true);
            Vector3 _nextPoint = _brain.Patrol.GetNextPoint();
            _brain.Movement.SetMoveTarget(_nextPoint);
            
        };
        OnUpdate += () =>
        {
            _brain.Movement.MoveTo();
        };
        
    }
}
