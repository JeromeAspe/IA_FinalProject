using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_IADetectionData 
{
    int angle = 0;
    float maxDistance = 0, currentDistance = 0;
    Transform originTransform = null;


    bool isObstacle = false;
    ITarget target = null;
    FP_Obstacle cover = null;

    public ITarget Target => target;
    public FP_Obstacle Cover => cover;
    public bool TargetDetected { get; set; } = false;
    public bool CoverDetected { get; set; } = false;
    public Vector3 StartPosition { get; set; } = Vector3.zero;

    public FP_IADetectionData(int _angle,float _maxDistance,Transform _originTransform)
    {
        angle = _angle;
        maxDistance = _maxDistance;
        originTransform = _originTransform;
    }

    public void Detection(LayerMask _playerMask,LayerMask _obstacleMask,float _offset)
    {
        StartPosition = originTransform.position + Vector3.up * _offset;
        isObstacle = Physics.Raycast(originTransform.position+Vector3.up*_offset, GetDirectionRay(), out RaycastHit _hitObstacle, maxDistance, _obstacleMask);
        if(isObstacle)
        {
            currentDistance = _hitObstacle.distance;
        }
        FP_Obstacle _cover = _hitObstacle.collider?.GetComponentInParent<FP_Obstacle>();
        CoverDetected = _cover != null;
        cover = _cover;
        bool _isPlayer = Physics.Raycast(originTransform.position + Vector3.up * _offset, GetDirectionRay(), out RaycastHit _hitPlayer, GetLength(), _playerMask);
        ITarget _target =  _hitPlayer.collider?.GetComponentInParent<ITarget>();
        TargetDetected = _target != null && !_target.IsDead;
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
        Debug.DrawRay(StartPosition, GetDirectionRay() * GetLength(),Color.green);
    }
}
