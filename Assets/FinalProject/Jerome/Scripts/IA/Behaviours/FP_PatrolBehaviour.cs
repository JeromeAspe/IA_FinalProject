using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FP_PatrolBehaviour : MonoBehaviour
{
    [SerializeField] List<Vector3> points = new List<Vector3>();
    [SerializeField] int index = 0;
    [SerializeField] NavMeshAgent agent = null;

    [SerializeField] bool isRandomPos = false;
    [SerializeField,Range(1,10)] float radiusRandomPos = 10;
    public bool IsValid => points.Count > 1;
    public Vector3 GetNextPoint()
    {
        if (!IsValid) return Vector3.zero;
        if (!isRandomPos)
        {
            NextPoint();
            return points[index];
        }
        return GetRandomPoint();
        
    }
    Vector3 GetRandomPoint()
    {
        float _angle = Random.Range(0, 360);
        float _x = Mathf.Cos(_angle) *radiusRandomPos + transform.position.x ;
        float _y = transform.position.y;
        float _z = Mathf.Sin(_angle)*radiusRandomPos + transform.position.z;
        return new Vector3(_x, _y, _z);
    }
    public void NextPoint()
    {
        index++;
        if(index >= points.Count)
        {
            index = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (!IsValid) return;
        for(int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawWireCube(points[i], Vector3.one*.5f);
            if (i < points.Count - 1)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
            }
        }
        
        
    }

}
