using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Player : FP_PlayerBehaviour, IHandledItem<int>, ITarget
{
	[SerializeField] int id = 0;
	[SerializeField, Range(0, 100)] int damage = 10;
	[SerializeField] bool isEnable = true;
	[SerializeField] ECameraType cameraType = ECameraType.None;
	[SerializeField] bool die = false;

	[Header("Scripts")]
	[SerializeField] Animator mecanim = null;
	[SerializeField] FP_PlayerRootMovements movement = null;
	[SerializeField] FP_PlayerShooter shooter = null;
	[SerializeField] FP_CameraSettings playerCameraSettings = new FP_CameraSettings();
	[SerializeField] Transform respawnPoint = null;

	[Header("Parameters")]
	[SerializeField] string respawnParameter = "respawn", deadParameter = "dead", shootParameter = "shoot", reloadParameter = "reload";

	public int ID => id;
	public bool IsValid => mecanim && movement && shooter;
	public bool IsEnabled => isEnable;

	public FP_PlayerShooter PlayerShooter => shooter;
	public Vector3 PlayerPosition => transform.position;
	public Vector3 CameraPosition => playerCameraSettings.TargetPosition + playerCameraSettings.Offset;



	public void SetDie() => die = IsDead;

	public void SetID(int _id) => id = _id;
	public void Disable()
	{
		isEnable = false;
		FP_CameraManager.Instance?.Disable($"Player {ID}");
	}

	private void Start()
	{
		InitHandledItem();
		OnLife += (life) => FP_UIManager.Instance?.UpdatePlayerHealthSlider(life);
		OnLife?.Invoke(life);
		

	}

	private void Update()
	{
		//Respawn();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		RemoveHandledItem();
	}

	public void Enable()
	{
		isEnable = true;
		FP_CameraManager.Instance?.Enable($"Player {ID}");
	}

	public void InitHandledItem()
	{
		InitFSM();
		InitCamera();
		FP_PlayerManager.Instance?.Add(this);
	}

	void Respawn()
	{
		if(die)
		{
			Debug.Log("doit respawn");
		this.gameObject.transform.position = respawnPoint.position;
		}

	}
	void InitFSM()
	{
		if (!IsValid) return;

		Respawn();

		OnDie += () =>
		{
			StartCoroutine(Dead());
		};


		shooter.OnShoot += () =>
		{			
			mecanim.SetTrigger(shootParameter);
			if (shooter.BulletsNumberMax != shooter.CurrentBulletsNumber)
				shooter.SetReload();
		};

		//à appeler quand que tu reload
		shooter.OnReload += () =>
		{
			mecanim.SetTrigger(reloadParameter);		
		};

		
	}

	IEnumerator Dead()
	{
		mecanim.applyRootMotion = false;
		mecanim.SetTrigger(deadParameter);
		yield return new WaitForSeconds(5);
		Respawn();
		yield return RespawnF();
	}

	IEnumerator RespawnF()
	{
		
		
		SetPositionRespawn();
		AddLife(maxLife);
		yield return new WaitForSeconds(1);
		mecanim.SetBool(respawnParameter, true);
		mecanim.applyRootMotion = true;
		yield return null;
	}

	public void SetPositionRespawn()
    {
		
		gameObject.transform.position = respawnPoint.position;
		
		Debug.Log(transform.position);
	}

	
	void InitCamera()
	{
		switch (cameraType)
		{
			case ECameraType.FPS:
				//FP_CameraManager.Instance?.CreateCamera(ECameraType.FPS, transform, $"{ID}");
				break;
			case ECameraType.None:
				break;
		}

	}

	public void RemoveHandledItem()
	{
		FP_PlayerManager.Instance?.Remove(this);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();
		if (Application.isPlaying) return;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(CameraPosition, Vector3.one / 2);
		Gizmos.DrawLine(PlayerPosition, CameraPosition);
	}

}
