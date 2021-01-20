using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_IAPlayer : FP_PlayerBehaviour
{
    [SerializeField,Range(1,100)] float wondedPercentageCover = 60;
    public event Action OnWounded = null;

    public bool IsWounded => wondedPercentageCover/100 > life / maxLife;
    public override void SetDamage(float _damage)
    {
        base.SetDamage(_damage);
        if (IsWounded)
            OnWounded?.Invoke();
        
    }
}
