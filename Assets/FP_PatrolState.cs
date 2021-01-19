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
            Vector3 _nextPoint = _brain.Patrol.GetNextPoint();
            _brain.Movement.SetMoveTarget(_nextPoint);
            //_brain.Movement.InvokeRepeating("MoveTo", 0, .5f);
        };
        OnUpdate += () =>
        {
            _brain.Movement.MoveTo();
        };
        
    }
}
