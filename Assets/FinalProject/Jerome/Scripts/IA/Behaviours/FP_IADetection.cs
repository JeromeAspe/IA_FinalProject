using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FP_IADetection : MonoBehaviour
{
    public event Action OnUpdateDebug = null;
    public event Action OnUpdateDetection = null;
    public event Action<ITarget> OnTargetDetected = null;
    public event Action<Vector3> OnTargetLost = null;

    [SerializeField,Range(0,100)] float maxDistance = 10;
    [SerializeField, Range(0, 180)] int angle = 90;
    [SerializeField, Range(.05f, 2)] float detectionTickRate = .2f;

    FP_IADetectionData[] rays = null;
    [SerializeField] LayerMask playerLayer = 0, obstacleLayer = 0;

    ITarget lastTarget = null;

    private void Start()
    {
        GenerateRays();
        InvokeRepeating("UpdateDetection", 0, detectionTickRate);
        OnTargetDetected += (_target) =>
        {
            //Debug.Log($"position {_target.TargetPosition}");
        };
        OnTargetLost += (pos) =>
        {
            //Debug.Log($"Lost {pos}");
        };
    }
    void GenerateRays()
    {
        rays = new FP_IADetectionData[angle];
        int index = 0;
        for(int i = -angle / 2; i < angle / 2; i++)
        {
            FP_IADetectionData _data = new FP_IADetectionData(i, maxDistance, transform);
            rays[index] = _data;
            OnUpdateDebug+= _data.DrawDetectionRay;
            OnUpdateDetection += () =>
            {
                _data.Detection(playerLayer, obstacleLayer);
                CheckDetection();
            };
            index++;
        }
    }
    void CheckDetection()
    {
        bool _playerDetected = rays.Any(_ray => _ray.TargetDetected);
        if(!_playerDetected && lastTarget!=null)
        {
            OnTargetLost?.Invoke(lastTarget.TargetPosition);
            lastTarget = null;
        }
        if (_playerDetected)
        {
            FP_IADetectionData _data = rays.FirstOrDefault(_ray => _ray.TargetDetected);
            lastTarget = _data.Target;
            OnTargetDetected?.Invoke(_data.Target);
        }
    }

    void UpdateDetection()
    {
        OnUpdateDetection?.Invoke();
    }
    void UpdateDebug()
    {
        OnUpdateDebug?.Invoke();
    }
    private void Update()
    {
        UpdateDebug();
    }
    private void OnDestroy()
    {
        OnUpdateDebug = null;
        OnUpdateDetection = null;
        OnTargetDetected = null;
        OnTargetLost = null;
    }

}
