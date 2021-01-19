using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Player : MonoBehaviour, IHandledItem<int>, ITarget
{
	public event Action<bool> OnNeedHeal;
	public event Action OnDie;
	public event Action<float> OnLife;



	[SerializeField] int id = 0;
	[SerializeField, Range(0, 100)] int life = 100;
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
	public bool IsDead => Life > 0;
	public bool NeedHeal => Life != 100;


	public Vector3 PlayerPosition => transform.position;
	public Vector3 CameraPosition => playerCameraSettings.TargetPosition + playerCameraSettings.Offset;
	public Vector3 TargetPosition => transform.position;


	public float Life
	{
		get => life;
		set
		{
			life = (int)value;
			life = Mathf.Clamp(life, 0, 100);
			OnNeedHeal?.Invoke(NeedHeal);
			OnLife?.Invoke(life);
			if (IsDead)
				OnDie?.Invoke();
		}
	}



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
	void OnDestroy()
	{
		RemoveHandledItem();
		OnLife = null;
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

		//à appeler que quand tu moves
		movement.OnMove += () =>
		{
			fsm.SetBool(walkParameter, true);
			
		};


		shooter.OnShoot += () =>
		{
			fsm.SetBool(walkParameter, false);
			fsm.SetBool(shootParameter, true);
	
		};

		//à appeler quand que tu reload
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

	void OnDrawGizmos()
	{
		if (Application.isPlaying) return;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(CameraPosition, Vector3.one / 2);
		Gizmos.DrawLine(PlayerPosition, CameraPosition);
	}

	public void SetDamage(float _damage)
	{
		damage = (int)_damage;
	}

	public void AddLife(float _life)
	{
		life += (int)_life;
	}
}
