using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Player : MonoBehaviour, IHandledItem<int>
{
	[SerializeField] int id = 0;
	[SerializeField] bool isEnable = true;
	[SerializeField] FP_CameraSettings playerCameraSettings = new FP_CameraSettings();
	public int ID => id;

	public bool IsValid => true;
	public bool IsEnabled => isEnable;
	public Vector3 PlayerPosition => transform.position;
	public Vector3 CameraPosition => playerCameraSettings.TargetPosition + playerCameraSettings.Offset;

	public void SetID(int _id) => id = _id;
	public void Disable()
	{
		isEnable = false;
		FP_CameraManager.Instance?.Disable($"Player {ID}");
	}

	public void Enable()
	{
		isEnable = true;
		FP_CameraManager.Instance?.Enable($"Player {ID}");
	}

	public void InitHandledItem()
	{
		InitCamera();
		FP_PlayerManager.Instance?.Add(this);
	}

	void InitCamera()
	{
		//FP_CameraManager.Instance?.CreateCamera<FP_CameraFPSBehaviour>($"Player{ID}", playerCameraSettings, transform);

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
}
