﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Obstacle : MonoBehaviour
{
    [SerializeField] Vector3 target = Vector3.zero;
    [SerializeField] FP_CoverSite[] covers = null;
    [SerializeField] int id = 0;


    public int ID => id;
    public void SetTarget(Vector3 _target) => target = _target;
    public Vector3 TargetPos => GetNormalizedVector(target);
    
    private void Start()
    {
        covers = GetComponentsInChildren<FP_CoverSite>();
    }
    Vector3 GetNormalizedVector(Vector3 _position)
    {
        return new Vector3(_position.x, transform.position.y, _position.z);
    }
    public FP_CoverSite GetBestCoverSide()
    {
        float _angle = 180;
        FP_CoverSite _bestCover = null;
        for(int i = 0; i < covers.Length; i++)
        {
            float _coverAngle = GetAngle(covers[i]);
            if (_coverAngle < _angle)
            {
                _bestCover = covers[i];
                _angle = _coverAngle;
            }
        }
        //Debug.Log(_angle);
        return _bestCover;
    }
    float GetAngle(FP_CoverSite _cover)
    {
        Vector3 _dir = GetNormalizedVector(_cover.transform.position) - TargetPos;
        //Debug.Log(Mathf.Abs(Vector3.Angle(_cover.transform.forward,_dir)));
        return Mathf.Abs(Vector3.Angle(_cover.transform.forward, _dir));
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < covers.Length; i++)
        {
            Gizmos.DrawLine(covers[i].transform.position, target);
        }
    }
}
