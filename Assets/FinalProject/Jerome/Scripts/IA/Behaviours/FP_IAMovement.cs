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
    [SerializeField,Range(0,20)] float moveSpeed = 2;
    [SerializeField,Range(0,100)] float rotateSpeed = 10;
    [SerializeField] NavMeshAgent agent = null;

    
    public void SetMoveTarget(Vector3 _target)
    {
        moveTarget = _target;
    }
    public void MoveTo()
    {
        if (IsAtRange())
        {
            OnTargetReached?.Invoke();
            return;
        }
        agent.Move(moveTarget);
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * moveSpeed);
    }
    public void RotateTo()
    {
        Quaternion _angle = Quaternion.LookRotation(transform.position, moveTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _angle,Time.deltaTime*rotateSpeed);
    }
    public bool IsAtRange()
    {
        return Vector3.Distance(moveTarget, transform.position) < isAtPosDistance;
    }

}
