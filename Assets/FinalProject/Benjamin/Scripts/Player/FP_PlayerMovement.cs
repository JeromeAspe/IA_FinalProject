using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FP_PlayerMovement : MonoBehaviour
{
	public event Action OnUpdatePlayer = null;
	public event Action OnMove = null;


	#region Serialize Fields
	[SerializeField] CharacterController controller = null;
	[SerializeField] bool useClampValue = true;
	[SerializeField] float clampMaxValue = 60, clampMinValue = 20;
	[SerializeField, Range(0, 10)] float moveSpeed = 4;
	[SerializeField, Range(0, 10)] float rotateSpeed = 2;


	#endregion


	Vector3 targetPosition = Vector3.zero;
	Vector3 currentPosition => transform.position;
	float horizontal = 0, vertical = 0;

	float rotateY = 0, rotateX = 0;

	public bool IsValid => controller;

	private void Awake()
	{
		OnUpdatePlayer += () =>
		{
			
		};

	}

	private void Start()
	{
		Init();
		OnMove?.Invoke();

	}

	private void Update()
	{
		OnUpdatePlayer?.Invoke();
		MoveTo();
		RotateTo();
	}

	void Init()
	{

		FP_InputManager.Instance?.RegisterAxis(AxisAction.HorizontalMove, UpdateHorizontalMove);
		FP_InputManager.Instance?.RegisterAxis(AxisAction.VerticalMove, UpdateVerticalMove);
		FP_InputManager.Instance?.RegisterAxis(AxisAction.HorizontalAxis, UpdateHorizontalRotate);
		FP_InputManager.Instance?.RegisterAxis(AxisAction.VerticalAxis, UpdateVerticalRotate);
		controller = GetComponent<CharacterController>();
	}

	#region Update Movement and Rotation

	void UpdateHorizontalMove(float _h) => horizontal = _h * moveSpeed;

	void UpdateVerticalMove(float _v) => vertical = _v * moveSpeed;

	void UpdateHorizontalRotate(float _h)
	{
		rotateY += _h * rotateSpeed;
		rotateY %= 360;
	}

	void UpdateVerticalRotate(float _v)
	{
		rotateX += _v * rotateSpeed;
		float _clampValue = useClampValue ? clampMaxValue : 60;
		rotateX = Mathf.Clamp(rotateX, -_clampValue, clampMinValue);
	}
	#endregion


	#region Movement and Rotation
	void RotateTo() => transform.eulerAngles = new Vector3(rotateX, rotateY, 0);

	void MoveTo()
	{
		targetPosition = (transform.right * horizontal) + (transform.forward * vertical);
		targetPosition.y -= 9.81f; //la gravité
		controller.Move(targetPosition* ( moveSpeed * Time.deltaTime));

	}
	#endregion



	private void OnDestroy()
	{
		OnUpdatePlayer -= () =>
		{
			MoveTo();
			RotateTo();
		};
		//FP_InputManager.Instance?.UnRegisterAxis(AxisAction.HorizontalMove, UpdateHorizontalMove); 
		//FP_InputManager.Instance?.UnRegisterAxis(AxisAction.VerticalMove, UpdateVerticalMove); 
		//FP_InputManager.Instance?.UnRegisterAxis(AxisAction.HorizontalAxis, UpdateHorizontalRotate); 
		//FP_InputManager.Instance?.UnRegisterAxis(AxisAction.VerticalAxis, UpdateVerticalRotate); 
		//FP_InputManager.Instance?.UnRegisterButton(ButtonAction.Fire,(fire) => shooter.Shoot(fire)); 
		//FP_InputManager.Instance?.UnRegisterButton(ButtonAction.Reload,(reload) => shooter.Reload(reload)); 
	}
	#region Gizmos
	private void OnDrawGizmos()
	{
		DebugPositionMovement();
		DebugRotation();
	}


	void DebugPositionMovement()
	{
		Vector3 _movement = currentPosition + new Vector3(targetPosition.x, 0, targetPosition.z);
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(_movement, .3f);
		Gizmos.DrawLine(currentPosition, _movement);
	}

	void DebugRotation()
	{
		Gizmos.color = Color.green;
		Vector3 _forwardAxis = transform.position + transform.forward * 2;
		Gizmos.DrawSphere(_forwardAxis, .1f);
		Gizmos.DrawLine(transform.position, _forwardAxis);
	}
	#endregion
}
