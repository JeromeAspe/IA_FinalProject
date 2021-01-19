using System;

public interface IStats
{
    event Action<bool> OnNeedHeal;
    event Action OnDie;
    

    bool IsDead { get; }
    bool NeedHeal { get; }
    float Life { get; }

    void SetDamage(float _damage);
    void AddLife(float _life);
}
