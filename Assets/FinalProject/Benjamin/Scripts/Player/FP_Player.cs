﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Player : MonoBehaviour, IHandledItem<int>, ITarget
{
    [SerializeField] int id = 0;
    [SerializeField, Range(0, 100)] int life = 100;
    [SerializeField, Range(0, 100)] int damage = 10;
    [SerializeField] bool isEnable = true;
    [SerializeField] FP_CameraSettings playerCameraSettings = new FP_CameraSettings();
    [SerializeField] ECameraType cameraType = ECameraType.None;

    public event Action<bool> OnNeedHeal;
    public event Action OnDie;
    public event Action<float> OnLife;

    public int ID => id;

    public bool IsValid => true;
    public bool IsEnabled => isEnable;
    public Vector3 PlayerPosition => transform.position;
    public Vector3 CameraPosition => playerCameraSettings.TargetPosition + playerCameraSettings.Offset;

    public Vector3 TargetPosition => transform.position;

    public bool IsDead => Life > 0;

    public bool NeedHeal => Life != 100;

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

        InitCamera();
        FP_PlayerManager.Instance?.Add(this);
    }

    void InitCamera()
    {
        switch (cameraType)
        {
            case ECameraType.FPS:
                //FP_CameraManager.Instance?.CreateCamera<FP_CameraFPSBehaviour>($"Player{ID}", playerCameraSettings, transform);
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
