using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_IADetectionData 
{
    int angle = 0;
    float maxDistance = 0, currentDistance = 0;

    Transform originTransform = null;


    bool isObstacle = false;


    public FP_IADetectionData(int _angle,float _maxDistance,Transform _originTransform)
    {
        angle = _angle;
        maxDistance = _maxDistance;
        originTransform = _originTransform;
    }

    public void Detection(LayerMask _playerMask,LayerMask _obstacleMask)
    {
        isObstacle = Physics.Raycast(originTransform.position, GetDirectionRay(), out RaycastHit _hitObstacle, maxDistance, _obstacleMask);
        bool _isPlayer = Physics.Raycast(originTransform.position, GetDirectionRay(), out RaycastHit _hitPlayer, maxDistance, _playerMask);

        if(_isPlayer && !IsPlayerHidden(_hitPlayer.point, _hitObstacle.point))
        {

        }
    }

    public float GetLength()
    {
        return isObstacle ? currentDistance : maxDistance;
    }
    bool IsPlayerHidden(Vector3 _hitPlayer,Vector3 _hitObstacle)
    {
        return Vector3.Distance(_hitPlayer, originTransform.position) > Vector3.Distance(_hitObstacle, originTransform.position);
    }
    public Vector3 GetDirectionRay()
    {
        return Quaternion.AngleAxis(angle, originTransform.up) * originTransform.forward; 
    }
}
