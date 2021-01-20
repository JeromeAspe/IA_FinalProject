using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class FP_CoverDetection : MonoBehaviour
{
    public event Action OnUpdateDebug = null;
    public event Action OnUpdateDetection = null;
    public event Action<FP_ObstacleTest> OnTargetDetected = null;

    [SerializeField, Range(0, 100)] float maxDistance = 10;
    [SerializeField, Range(0, 180)] int angle = 90;
    [SerializeField, Range(.05f, 2)] float detectionTickRate = .2f;

    FP_CoverDetectionData[] rays = null;
    [SerializeField] LayerMask coverLayer = 0, obstacleLayer = 0;


    private void Start()
    {
        GenerateRays();
        //InvokeRepeating("UpdateDetection", 0, detectionTickRate);
        OnTargetDetected += (_target) =>
        {
            Debug.Log("detected");
        };
    }
    void GenerateRays()
    {
        rays = new FP_CoverDetectionData[angle];
        int index = 0;
        for (int i = -angle / 2; i < angle / 2; i++)
        {
            FP_CoverDetectionData _data = new FP_CoverDetectionData(i, maxDistance, transform);
            rays[index] = _data;
            OnUpdateDebug += _data.DrawDetectionRay;
            OnUpdateDetection += () =>
            {
                _data.Detection(coverLayer, obstacleLayer);
                CheckDetection();
            };
            index++;
        }
    }
    void CheckDetection()
    {
        bool _playerDetected = rays.Any(_ray => _ray.TargetDetected);
        if (_playerDetected)
        {
            FP_CoverDetectionData _data = rays.FirstOrDefault(_ray => _ray.TargetDetected);
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
    }
}
