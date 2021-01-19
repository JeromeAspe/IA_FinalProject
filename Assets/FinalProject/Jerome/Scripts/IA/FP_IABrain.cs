﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FP_IABrain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;
    [SerializeField] FP_IAPlayer iaPlayer = null;
    [SerializeField] FP_IAMovement movement = null;

    public Animator FSM => fsm;
    public FP_IAPlayer IaPlayer => iaPlayer;
    public FP_IAMovement Movement => movement;
    public bool IsValid => fsm && iaPlayer && movement;

    protected virtual void Start()
    {
        Init();
    }
    protected virtual void Init()
    {
        if (!fsm)
        {
            fsm = GetComponent<Animator>();
        }
        if (!IsValid) return;
        InitBrain();
    }
    protected virtual void InitBrain()
    {
        
        FP_State[] _states = fsm.GetBehaviours<FP_State>();
        //Debug.Log(_states.Length);
        for (int i = 0; i < _states.Length; i++)
        {
            _states[i].InitState(this);
        }
    }
    protected virtual void InitMovements()
    {
        Movement.OnTargetReached += () =>
        {
            //Wait
        };
    }
}
