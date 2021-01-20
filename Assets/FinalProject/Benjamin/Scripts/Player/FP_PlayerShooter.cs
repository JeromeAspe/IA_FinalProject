﻿using System.Collections;
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

	// FX
	[SerializeField, Range(0, 10)] float durationFx = .5f;
	[SerializeField] GameObject shootFX = null;
	[SerializeField] GameObject shootHitFX = null;
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
			return weapon.transform.position + weapon.transform.forward * 1.4f;
		}
	}

	public Vector3 ShootPointWithDistance
	{
		get
		{
			if (!IsValid) return transform.position;
			return weapon.transform.position + weapon.transform.forward * shootDistance;

		}
	}


	public GameObject ShootFX => shootFX;
	public GameObject ShootHitFX => shootHitFX;

	private void Awake() => Init();

    private void Start()
    {
		//OnReload?.Invoke();
	}
	void Update()=>SetTimer();

	void OnDestroy() => Remove();

	void Init()
	{
		currentBulletsNumber = bulletsNumberMax;

		//OnShoot += () => SetReload();
		OnReload += () =>
		{ 
			Debug.Log($"{currentBulletsNumber} / {bulletsNumberMax}");
			SetReload();
			FP_UIManager.Instance?.UpdateWeaponCapacityUI(currentBulletsNumber, bulletsNumberMax);
		};
		OnShoot += () =>
		{
			InstantiateFX(ShootFX, weapon.transform.position, .1f);
			FP_UIManager.Instance?.UpdateWeaponCapacityUI(currentBulletsNumber, bulletsNumberMax);
		};
		OnShootHit += () => InstantiateFX(ShootHitFX, transform.position, .1f); //blood
	}


	void Remove()
	{
		OnShoot = null;
		OnShootHit = null;
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
			Debug.Log(_fireHit);
			if (!_fireHit) return;
			lastHitPoint = _hit.point;
			Debug.Log(_hit.point);
			Debug.Log("touché l'ennemi");
			OnShootHit?.Invoke();
			Debug.Log("Number of ammo :" + bulletsNumberMax);

		}

		else Debug.LogError("No ammo");
	}

	public void Reload(bool _action)
	{
		if (!_action || !IsValid || !isReload) return;
		if (bulletsNumberMax <= 0)
		{
			OnReload?.Invoke();
			bulletsNumberMax = 5;//put a maxvalue
		}
	}
	
	void SetReload()
	{
		currentBulletsNumber = bulletsNumberMax;
		isReload = true;
	}

	
	
	public void InstantiateFX(GameObject _fx, Vector3 _position, string _audioResources, float _sizeFX = 1)
	{
		GameObject _effect = Instantiate(_fx, _position, Quaternion.identity);
		_effect.transform.localScale *= _sizeFX;
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(_audioResources), _position);
		Destroy(_effect, durationFx);
	}

	//same but without Audio
	public void InstantiateFX(GameObject _fx, Vector3 _position, float _sizeFX = 1)
	{
		GameObject _effect = Instantiate(_fx, _position, Quaternion.identity);
		_effect.transform.localScale *= _sizeFX;
		Destroy(_effect, durationFx);
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
		Gizmos.DrawRay(weapon.transform.position, weapon.transform.forward * shootDistance);
		Gizmos.DrawSphere(weapon.transform.position+ weapon.transform.forward * shootDistance, .1f);
	}
	void DebugShootPoint()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(ShootPointWithDistance, .1f);
	}
}
