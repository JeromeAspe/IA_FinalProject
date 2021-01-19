using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_FightSystem : MonoBehaviour, IShooter
{
    public Action OnAttack = null;
    public event Action OnShoot = null;
    public event Action OnShootHit = null;
    public event Action OnReload = null;

    Transform targetTransform = null;
    ITarget target = null;
    
    [SerializeField,Range(0,100)] float shootDistance = 10;
    [SerializeField,Range(0,5)] float reloadTimeValue = 1;
    [SerializeField,Range(0,10)] float fireRate = 1;
    [SerializeField,Range(0,50)] int bulletsMax = 20;
    [SerializeField,Range(0,50)] int currentBulletNB = 20;
    [SerializeField,Range(0,100)] float damage = 2;
    bool canShoot = true;
    float timer = 0;
    public void SetTarget(ITarget _target)
    {
        target = _target;
    }
    public bool IsValid => targetTransform && target!= null;
    private void Start()
    {
        if (!targetTransform) return;
        target = targetTransform.GetComponent<ITarget>();
        currentBulletNB = bulletsMax;
        
        OnShoot += () =>
        {
            Shoot(true);
        };
        OnAttack += OnShoot;
    }

   

    public float ShootDistance => shootDistance;

    public float ReloadTimeValue => reloadTimeValue;

    public float FireRate => fireRate;

    public int BulletsNumberMax => bulletsMax ;

    public float Timer => timer;


    public void SetTimer()
    {
        if (canShoot) return;
        timer += Time.deltaTime;
        if(timer> fireRate)
        {
            OnShoot?.Invoke();
            canShoot = false;
            timer = 0;
        }
    }

    public void Shoot(bool _action)
    {
        if (!IsValid || !canShoot) return;
        target.SetDamage(damage);
        currentBulletNB--;
    }

    public void UpdateShootState()
    {
        SetTimer();
        currentBulletNB--;
        if (currentBulletNB == 0)
        {
            OnReload?.Invoke();
        }
    }
    public void Reload()
    {
        if (!IsValid) return;
        currentBulletNB = bulletsMax;
        timer = 0;
        canShoot = false;
    }
    private void OnDestroy()
    {
         OnShoot =null;
         OnShootHit = null;
        OnReload = null;
    }

}
