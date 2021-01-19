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


	[Header("Scripts")]
	[SerializeField] Animator fsm = null;
	[SerializeField] FP_PlayerMovement movement = null;
	[SerializeField] FP_PlayerShooter shooter= null;
	[SerializeField] FP_CameraSettings playerCameraSettings = new FP_CameraSettings();


	[Header("Parameters")]
	[SerializeField] string walkParameter = "Is_Walk", shootParameter= "Is_Shoot", reloadParameter= "Is_Reload";

	public int ID => id;
	public bool IsValid => fsm && movement && shooter;
	public bool IsEnabled => isEnable;


	public Vector3 PlayerPosition => transform.position;
	public Vector3 CameraPosition => playerCameraSettings.TargetPosition + playerCameraSettings.Offset;





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
		InitShootInput();
		InitCamera();
		FP_PlayerManager.Instance?.Add(this);
	}


	void InitFSM()
	{
		if (!IsValid) return;

		movement.OnMove += () =>
		{
			fsm.SetBool(walkParameter, true);
			
		};

		shooter.OnShoot += () =>
		{
			fsm.SetBool(walkParameter, false);
			fsm.SetBool(shootParameter, true);
	
		};

		shooter.OnReload += () =>
		{
			fsm.SetBool(reloadParameter, true);
			fsm.SetBool(walkParameter, false);
			fsm.SetBool(shootParameter, false);
		};
	}

	void InitShootInput()
	{
		FP_InputManager.Instance?.RegisterButton(ButtonAction.Fire, (fire) => shooter.Shoot(fire));
		FP_InputManager.Instance?.RegisterButton(ButtonAction.Reload, (reload) => shooter.Reload(reload));
	}
	void InitCamera()
	{
		switch (cameraType)
		{
			case ECameraType.FPS:
				FP_CameraManager.Instance?.CreateCamera(ECameraType.FPS, transform, $"{ID}");
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
