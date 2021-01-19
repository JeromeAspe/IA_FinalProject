using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class FP_IAMovement : MonoBehaviour
{
    public event Action<bool> OnMove = null;
    public event Action OnTargetReached = null;

    [SerializeField] Vector3 moveTarget = Vector3.zero;
    [SerializeField,Range(0,10)] float isAtPosDistance = 1;
     float moveSpeed = 2;
    float rotateSpeed = 10;
    [SerializeField] NavMeshAgent agent = null;

    private void Start()
    {
        moveSpeed = agent.speed;
        rotateSpeed = agent.angularSpeed;
    }
    public void SetStateNav(bool _state)
    {
        if (!_state)
        {
            agent.speed = 0;
            agent.angularSpeed = 0;
        }
        else
        {
            agent.speed = moveSpeed;
            agent.angularSpeed = rotateSpeed;
        }


    }
    public void SetMoveTarget(Vector3 _target)
    {
        moveTarget = _target;
        agent.SetDestination(moveTarget);
        
    }
    public void MoveTo()
    {
        if (IsAtRange())
        {
            OnTargetReached?.Invoke();
            agent.SetDestination(transform.position);
            return;
        }
        

    }
    public void RotateTo()
    {
        Quaternion _angle = Quaternion.LookRotation(moveTarget - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _angle, Time.deltaTime * rotateSpeed);
    }
    public bool IsAtRange()
    {
        return Vector3.Distance(moveTarget, transform.position) < isAtPosDistance;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(moveTarget, .5f);
    }

}
