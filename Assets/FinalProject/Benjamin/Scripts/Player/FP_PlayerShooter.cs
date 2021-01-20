using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FP_PlayerShooter : MonoBehaviour, IShooter, IEffects
{
    public event Action OnShoot;
    public event Action OnShootHit;
    public event Action OnReload;


    // Shoot
    [SerializeField] GameObject weapon = null;
    [SerializeField, Range(0, 100)] float shootDistance = 10;
    [SerializeField, Range(0, 15)] float reloadTimeValue = 5;
    [SerializeField, Range(0, 10)] float fireRate = 2;
    [SerializeField, Range(0, 10)] int bulletsNumberMax = 5;
    [SerializeField] FP_IAPlayer enemy = null;
    [SerializeField, Range(0,100)] int damage = 3;
    // FX
    [SerializeField, Range(0, 10)] float durationFx = .5f;
    [SerializeField] GameObject shootFX = null;
    [SerializeField] GameObject shootHitFX = null;
    [SerializeField] AudioClip shootSound = null;
    [SerializeField] AudioClip reloadSound = null;
    [SerializeField] LayerMask aiMask = 11;

    //Private
    float timer = 0;
    int currentBulletsNumber = 0;
    bool isReload = false;
    Vector3 lastHitPoint = Vector3.zero;



    // Interfaces
    public float ShootDistance => shootDistance;
    public float ReloadTimeValue => reloadTimeValue;
    public float FireRate => fireRate;
    public int BulletsNumberMax => bulletsNumberMax;
    public float Timer => timer;
    public float DurationFx => durationFx;

    public bool IsValid => weapon;

    public Vector3 ShootPoint
    {
        get
        {
            if (!IsValid) return transform.position;
            return weapon.transform.position + new Vector3(0, 0.1f, 0) + weapon.transform.forward * 1.4f;
        }
    }

    public Vector3 ShootPointWithDistance
    {
        get
        {
            if (!IsValid) return transform.position;
            return weapon.transform.position + new Vector3(0, 0.1f, 0) + weapon.transform.forward * shootDistance;


        }
    }


    public GameObject ShootFX => shootFX;
    public GameObject ShootHitFX => shootHitFX;

    private void Awake() => Init();

    private void Start()
    {
        FP_InputManager.Instance?.RegisterButton(ButtonAction.Fire, Shoot);
        FP_InputManager.Instance?.RegisterButton(ButtonAction.Reload, Reload);
        //OnReload?.Invoke();
    }
    void Update()
    {
        SetTimer();
    }

    void OnDestroy()
    {
        FP_InputManager.Instance?.UnRegisterButton(ButtonAction.Fire, Shoot);
        FP_InputManager.Instance?.UnRegisterButton(ButtonAction.Reload, Reload);
        Remove();
    }

    void Init()
    {
        currentBulletsNumber = bulletsNumberMax;

        //OnShoot += () => SetReload();
        OnReload += () =>
        {
            bulletsNumberMax += 10;
            SetReload();
            InstantiateSound(reloadSound, weapon.transform.position, 2);
            FP_UIManager.Instance?.UpdateWeaponCapacityUI(currentBulletsNumber, bulletsNumberMax);
        };
        OnShoot += () =>
        {
            InstantiateFX(ShootFX, weapon.transform.position + weapon.transform.forward, shootSound, 1);
            FP_UIManager.Instance?.UpdateWeaponCapacityUI(currentBulletsNumber, bulletsNumberMax);
        };
        OnShootHit += () => InstantiateFX(ShootHitFX, lastHitPoint, 2);//blood
    }


    void Remove()
    {
        OnShoot = null;
        OnShootHit = null;
        OnReload = null;
    }


    public void SetTimer()
    {
        if (!isReload || !IsValid) return;
        timer += Time.deltaTime;
        if (timer >= (currentBulletsNumber == 0 ? reloadTimeValue : fireRate))
        {
            isReload = false;
            if (currentBulletsNumber == 0) currentBulletsNumber = bulletsNumberMax;
            timer = 0;
        }
    }


    public void Shoot(bool _action)
    {
        if (!_action || !IsValid || isReload) return;

        if (bulletsNumberMax > 0)
        {

            bulletsNumberMax -= 1;
            OnShoot?.Invoke();
            bool _fireHit = Physics.Raycast(weapon.transform.position, ShootPointWithDistance, out RaycastHit _hit, shootDistance, aiMask);
            if (!_fireHit) return;
            lastHitPoint = _hit.point;
            enemy.Life -= damage;
            Debug.Log("touché l'ennemi");
            OnShootHit?.Invoke();

        }

        else Debug.LogError("No ammo");
    }

    public void Reload(bool _action)
    {
        if (!_action || !IsValid || !isReload) return;
        if (bulletsNumberMax <= 0)
        {
            SetReload();
            OnReload?.Invoke();
            bulletsNumberMax += currentBulletsNumber;//put a maxvalue
        }
    }

    public void SetReload()
    {
        currentBulletsNumber = bulletsNumberMax;
        isReload = true;
    }



    public void InstantiateFX(GameObject _fx, Vector3 _position, AudioClip _audioResources, float _sizeFX = 1)
    {
        GameObject _effect = Instantiate(_fx, _position, Quaternion.identity);
        _effect.transform.localScale *= _sizeFX;
        AudioSource.PlayClipAtPoint(_audioResources, _position);
        Destroy(_effect, durationFx);
    }


    public void InstantiateFX(GameObject _fx, Vector3 _position, float _sizeFX = 1)
    {
        GameObject _effect = Instantiate(_fx, _position, Quaternion.identity);
        _effect.transform.localScale *= _sizeFX;
        Destroy(_effect, durationFx);
    }

   public void InstantiateSound(AudioClip _audioressources,Vector3 _position, float _duration)
    {
        AudioSource.PlayClipAtPoint(_audioressources,_position, _duration);
    }

    public void SetWeaponActive()
    {
        if (!IsValid) return;
        weapon.SetActive(!weapon.activeSelf);
    }

    void OnDrawGizmos()
    {
        if (!IsValid) return;
        DebugShoot();
        DebugShootPoint();
    }
    void DebugShoot()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(weapon.transform.position + new Vector3(0, 0.1f, 0), weapon.transform.forward * shootDistance);
        Gizmos.DrawSphere(weapon.transform.position + new Vector3(0, 0.1f, 0) + weapon.transform.forward * shootDistance, .1f);
    }
    void DebugShootPoint()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(ShootPointWithDistance, .1f);
    }

}
