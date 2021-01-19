using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FP_State : StateMachineBehaviour
{
    public event Action OnEnter = null;
    public event Action OnUpdate = null;
    public event Action OnExit = null;

    FP_IABrain brain = null;


    public void InitState(FP_IABrain _brain)
    {
        brain = _brain;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        OnEnter?.Invoke();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        OnUpdate?.Invoke();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        OnExit?.Invoke();
    }

}
