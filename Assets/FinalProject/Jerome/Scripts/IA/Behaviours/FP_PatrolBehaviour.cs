using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FP_PatrolBehaviour : MonoBehaviour
{
    [SerializeField] List<Vector3> points = new List<Vector3>();
    [SerializeField] int index = 0;
    [SerializeField] NavMeshAgent agent = null;


    public bool IsValid => points.Count > 1;
    public Vector3 GetNextPoint()
    {
        if (!IsValid) return Vector3.zero;
        NextPoint();
        return points[index];
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
