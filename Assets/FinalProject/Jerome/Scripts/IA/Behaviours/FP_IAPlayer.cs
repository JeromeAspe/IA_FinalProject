using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_IAPlayer : FP_PlayerBehaviour
{
    [SerializeField] Vector3 respawn = Vector3.zero;
    [SerializeField,Range(1,100)] float wondedPercentageCover = 60;
    public event Action OnWounded = null;

    public bool IsWounded => wondedPercentageCover/100 > life / maxLife;
    public override void SetDamage(float _damage)
    {
        base.SetDamage(_damage);
        if (IsWounded)
            OnWounded?.Invoke();
        
    }
    public void Respawn()
    {
        transform.position = respawn;
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(respawn, Vector3.one);
    }
}
