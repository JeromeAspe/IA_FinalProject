using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_CoverDetectionData
{
    int angle = 0;
    float maxDistance = 0, currentDistance = 0;
    Transform originTransform = null;


    bool isObstacle = false;
    FP_ObstacleTest target = null;

    public FP_ObstacleTest Target => target;
    public bool TargetDetected { get; set; } = false;


    public FP_CoverDetectionData(int _angle, float _maxDistance, Transform _originTransform)
    {
        angle = _angle;
        maxDistance = _maxDistance;
        originTransform = _originTransform;
    }

    public void Detection(LayerMask _coverMask, LayerMask _obstacleMask)
    {
        isObstacle = Physics.Raycast(originTransform.position, GetDirectionRay(), out RaycastHit _hitObstacle, maxDistance, _obstacleMask);
        if (isObstacle)
        {
            currentDistance = _hitObstacle.distance;
        }
        bool _isCover = Physics.Raycast(originTransform.position, GetDirectionRay(), out RaycastHit _hitCover, GetLength(), _coverMask);
        FP_ObstacleTest _target = _hitCover.collider?.GetComponentInParent<FP_ObstacleTest>();
        TargetDetected = _target != null;
        target = _target;
    }

    public float GetLength()
    {
        return isObstacle ? currentDistance : maxDistance;
    }
    public Vector3 GetDirectionRay()
    {
        return Quaternion.AngleAxis(angle, originTransform.up) * originTransform.forward;
    }

    public void DrawDetectionRay()
    {
        Debug.DrawRay(originTransform.position, GetDirectionRay() * GetLength(), Color.green);
    }
}
