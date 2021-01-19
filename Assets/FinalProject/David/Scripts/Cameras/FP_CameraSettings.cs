﻿using System;
using UnityEngine;

[Serializable]
public class FP_CameraSettings
{
    [SerializeField, Header("Follow Target")] Transform target = null;
    [SerializeField, Header("Camera Movements Speed"), Range(0, 200)] float cameraMovementsSpeed = 5;
    [SerializeField, Header("Camera Rotation Speed"), Range(0, 200)] float cameraRotationSpeed = 10;

    [SerializeField, Header("X Offset"), Range(-20, 20)] float xOffset = 0;
    [SerializeField, Header("Y Offset"), Range(-20, 20)] float yOffset = 1.5f;
    [SerializeField, Header("Z Offset"), Range(-20, 20)] float zOffset = 0;

    public Transform Target => target;
    public Vector3 TargetPosition
    {
        get
        {
            if (!target) return Vector3.zero;
            return target.position;
        }
    }
    public Vector3 Offset
    {
        get
        {
            if (!target) return Vector3.one;
            return (target.right * xOffset) + (target.up * yOffset) + (target.forward * zOffset);
        }
    }
    public void SetCameraSpeed(float _movementsSpeed,float _rotationSpeed)
    {
        cameraMovementsSpeed = _movementsSpeed;
        cameraRotationSpeed = _rotationSpeed;
    }

    public void SetTarget(Transform _target) => target = _target;

    public void SetOffset(ECameraType _type)
    {
        if (!target) return;
        switch(_type)
        {
            case ECameraType.FPS:
                yOffset = target.up.y + 0.5f;
                zOffset = target.forward.z * 0;
                
                break;
            default: 
                break;
        }
    }

}

