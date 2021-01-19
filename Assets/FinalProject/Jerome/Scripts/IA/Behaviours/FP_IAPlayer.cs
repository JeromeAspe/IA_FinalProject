using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_IAPlayer : MonoBehaviour, ITarget
{
    public event Action<bool> OnNeedHeal;
    public event Action OnDie;
    public event Action<float> OnLife;

    [SerializeField] float life = 10;
    [SerializeField] float maxLife = 10;

    public Vector3 TargetPosition => transform.position;

    public bool IsDead => life<=0;

    public bool NeedHeal => life<maxLife;

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

    

    public void AddLife(float _life)
    {
        Life += _life;
    }

    public void SetDamage(float _damage)
    {
        Life -= life;
    }

    private void OnDestroy()
    {
        OnNeedHeal = null;
        OnDie = null;
        OnLife = null;
    }
}
