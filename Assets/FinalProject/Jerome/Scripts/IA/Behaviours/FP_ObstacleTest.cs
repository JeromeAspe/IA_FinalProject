using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_ObstacleTest : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] FP_CoverSite[] covers = null;


    public Vector3 TargetPos => GetNormalizedVector(target.position);
    
    private void Start()
    {
        covers = GetComponentsInChildren<FP_CoverSite>();
    }
    private void Update()
    {
        GetSideHidden();
    }
    Vector3 GetNormalizedVector(Vector3 _position)
    {
        return new Vector3(_position.x, transform.position.y, _position.z);
    }
    void GetSideHidden()
    {
        for(int i = 0; i < covers.Length; i++)
        {
            GetAngle(covers[i]);
        }
    }
    float GetAngle(FP_CoverSite _cover)
    {
        Vector3 _dir = GetNormalizedVector(_cover.transform.position) - TargetPos;
        Debug.Log(Mathf.Abs(Vector3.Angle(_cover.transform.forward,_dir)));
        return Mathf.Abs(Vector3.Angle(GetNormalizedVector(_cover.transform.position), TargetPos));
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < covers.Length; i++)
        {
            Gizmos.DrawLine(covers[i].transform.position, target.position);
        }
    }
}
