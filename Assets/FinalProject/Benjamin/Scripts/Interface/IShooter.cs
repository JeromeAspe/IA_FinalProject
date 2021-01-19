using System;

public interface IShooter 
{
    event Action OnShoot;
    event Action OnShootHit;
    float ShootDistance { get; }
    float ReloadTimeValue { get; }
    float FireRangeValue { get; }
    int BulletsNumberMax { get; }
    float Timer { get; }
    void SetTimer();
    void Shoot(bool _action);
}
