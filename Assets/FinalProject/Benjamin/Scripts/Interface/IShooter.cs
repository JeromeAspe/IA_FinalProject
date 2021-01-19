using System;

public interface IShooter 
{
    event Action OnShoot;
    event Action OnShootHit;
    float ShootDistance { get; }
    float ReloadTimeValue { get; }
    float FireRate { get; }
    int BulletsNumberMax { get; }
    float Timer { get; }
    void SetTimer();
    void Shoot(bool _action);
}
