using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class FP_PlayerBehaviour : MonoBehaviour,ITarget
{
    public event Action<bool> OnNeedHeal = null;
    public event Action OnDie = null;
    public Action<float> OnLife = null;
    public event Action OnHit = null;

    [SerializeField] protected float life = 10;
    [SerializeField] protected float maxLife = 10;

    public Vector3 TargetPosition => transform.position;

    public bool IsDead => life <= 0;

    public bool NeedHeal => life < maxLife;

    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
            life = Mathf.Clamp(life, 0, maxLife);
            OnLife?.Invoke(life);
            if (life <= 0)
            {
                OnDie?.Invoke();
            }

        }
    }



    public virtual void AddLife(float _life)
    {
        Life += _life;
    }

    public virtual void SetDamage(float _damage)
    {
        Life -= _damage;
        OnHit?.Invoke();
    }
    public void Start()
    {

    }

    protected virtual void OnDestroy()
    {
        OnNeedHeal = null;
        OnDie = null;
        OnLife = null;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 3);
        Gizmos.color = Color.Lerp(Color.red, Color.green, Life / maxLife);
        Gizmos.DrawSphere(transform.position + Vector3.up * 3, .3f);
    }
}
