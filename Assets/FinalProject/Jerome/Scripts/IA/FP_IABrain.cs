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
    


    string waitParameter = "wait";
    string patrolParameter = "patrol";
    string attackParameter = "attack";

    public string WaitParameter => waitParameter;
    public string PatrolParameter => patrolParameter;
    public Animator FSM => fsm;
    public FP_IAPlayer IaPlayer => iaPlayer;
    public FP_IAMovement Movement => movement;
    public FP_PatrolBehaviour Patrol => patrol;
    public FP_IADetection Detection => detection;
    public FP_FightSystem FightSystem => fightSystem;
    public bool IsValid => fsm && iaPlayer && movement && detection && fightSystem;

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
    }
    protected virtual void InitMovements()
    {
        Movement.OnTargetReached += () =>
        {
            fsm.SetBool(waitParameter, true);
        };
        detection.OnTargetDetected += (_target) =>
        {
            if (!_target.IsDead)
            {
                movement.SetMoveTarget(_target.TargetPosition);
                fsm.SetBool(PatrolParameter, false);
                fsm.SetBool(waitParameter, false);
            }
            else
            {
                fsm.SetBool(PatrolParameter, true);
                
            }
            
        };
    }
    protected virtual void InitDetection()
    {
        
        detection.OnTargetLost += (_position) =>
        {
            //Set research position
        };
    }
    protected virtual void InitFight()
    {
        detection.OnTargetDetected += (_target) =>
        {
            if (!_target.IsDead)
            {
                fsm.SetBool(attackParameter, true);
                fightSystem.SetTarget(_target);
            }
            else
            {
                fsm.SetBool(attackParameter, false);
            }
        };

    }
    private void Update()
    {
        fightSystem.UpdateShootState();
    }
}
