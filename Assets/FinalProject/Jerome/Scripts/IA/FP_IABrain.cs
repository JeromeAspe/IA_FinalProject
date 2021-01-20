using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FP_IABrain : MonoBehaviour
{
    [SerializeField] Animator fsm = null;
    [SerializeField] FP_IAPlayer iaPlayer = null;
    [SerializeField] FP_IAMovement movement = null;
    [SerializeField] FP_PatrolBehaviour patrol = null;
    [SerializeField] FP_IADetection detection = null;
    [SerializeField] FP_FightSystem fightSystem = null;
    [SerializeField] FP_SearchBehaviour chase = null;
    [SerializeField] FP_IAStats stats = new FP_IAStats();
    [SerializeField] FP_IAAnimations animations = null;
    [SerializeField] FP_CoverBehaviour coverBehaviour = null;



    string waitParameter = "wait";
    string patrolParameter = "patrol";
    string attackParameter = "attack";
    string chaseParameter = "chase";
    string resetParameter = "reset";
    string dieParameter = "die";
    string coverParameter = "cover";

    bool HasKilled = false;

    public string WaitParameter => waitParameter;
    public string PatrolParameter => patrolParameter;
    public string AttackParameter => attackParameter;
    public string ChaseParameter => chaseParameter;
    public string ResetParameter => resetParameter;
    public string DieParameter => dieParameter;
    public string CoverParameter => coverParameter;
    public Animator FSM => fsm;
    public FP_IAPlayer IaPlayer => iaPlayer;
    public FP_IAMovement Movement => movement;
    public FP_PatrolBehaviour Patrol => patrol;
    public FP_IADetection Detection => detection;
    public FP_FightSystem FightSystem => fightSystem;
    public FP_SearchBehaviour Chase => chase;
    public FP_IAStats Stats => stats;
    public FP_IAAnimations Animations => animations;
    public FP_CoverBehaviour CoverBehaviour => coverBehaviour;
    public bool IsValid => fsm && iaPlayer && movement && detection && fightSystem && chase && animations && coverBehaviour;

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
        InitMovements();
        InitDetection();
        InitFight();
        InitPlayerIA();
        InitAnimations();
        InitCoverDetection();
    }
    protected virtual void InitMovements()
    {
        Movement.OnTargetReached += () =>
        {
            Debug.Log("reached");
            fsm.SetBool(waitParameter, true);
            fsm.SetBool(coverParameter, false);
        };
        detection.OnTargetDetected += (_target) =>
        {
            if (!_target.IsDead && !fsm.GetBool(CoverParameter))
            {
                movement.SetMoveTarget(_target.TargetPosition);
                fsm.SetBool(AttackParameter, true);
                fsm.SetBool(PatrolParameter, false);
                fsm.SetBool(waitParameter, false);
                fsm.SetBool(chaseParameter, false);
            }
            else
            {
                fsm.SetBool(PatrolParameter, true);
                fsm.SetBool(chaseParameter, false);
                HasKilled = true;

            }
            
        };
        
    }
    protected virtual void InitDetection()
    {

        detection.OnTargetLost += (_pos) =>
        {
            if (HasKilled || IaPlayer.IsWounded)
            {
                HasKilled = false;
                return;
            }
            
            fsm.SetBool(PatrolParameter, false);
            fsm.SetBool(waitParameter, false);
            fsm.SetBool(chaseParameter, true);
            chase.SetLastSeenPosition(_pos);
        };
    }
    protected virtual void InitFight()
    {
        detection.OnTargetDetected += (_target) =>
        {
            if (!_target.IsDead)
            {
                coverBehaviour.SetTarget(_target.TargetPosition);
                fsm.SetBool(attackParameter, true);
                fightSystem.SetTarget(_target);
            }
            else
            {
                fsm.SetBool(attackParameter, false);
            }
        };
        detection.OnTargetLost += (_pos) =>
        {
            fsm.SetBool(attackParameter, false);
            if (iaPlayer.IsWounded)
            {
                fsm.SetBool(coverParameter, true);
                fsm.SetBool(patrolParameter, false);
            }
                
        };

    }
    public void InitPlayerIA()
    {
        iaPlayer.OnDie += () =>
        {
            fsm.SetBool(dieParameter, true);
        };
    }

    public void InitAnimations()
    {
        movement.OnMove += (_bool) =>
         {
             animations.SetWalkAnimation(_bool);
             animations.SetShootAnimation(false);
         };
        fightSystem.OnAttack += () =>
        {
            animations.SetShootAnimation(true);
        };
    }
    public void InitCoverDetection()
    {

        iaPlayer.OnWounded += () =>
        {
            fsm.SetBool(coverParameter, true);
            fsm.SetBool(patrolParameter, false);
        };
        detection.OnCoverDetected += (_cover) =>
        {
            coverBehaviour.Add(_cover);
        };

        
    }
    
    private void Update()
    {
        fightSystem.UpdateShootState();
    }
    public void ResetStates()
    {
        fsm.SetBool(resetParameter, true);
    }
}
