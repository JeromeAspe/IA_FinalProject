using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_SearchBehaviour : MonoBehaviour
{
    [SerializeField, Range(1, 20)] float searchRadius = 10;
    List<Vector3> historicPoints = new List<Vector3>();
    Vector3 lastSeenPosition = Vector3.zero, searchPoint = Vector3.zero;

    public float SearchRadius => searchRadius;
    public void SetLastSeenPosition(Vector3 _lastPosition)
    {
        lastSeenPosition = _lastPosition;
        historicPoints.Clear();
    }

    public Vector3 GetSearchPoint()
    {
        int _angle = Random.Range(0, 360);
        float _x = Mathf.Cos(_angle * Mathf.Deg2Rad) * searchRadius;
        float _y = 0;
        float _z = Mathf.Sin(_angle * Mathf.Deg2Rad) * searchRadius;
        searchPoint = new Vector3(_x, _y, _z) + lastSeenPosition;
        historicPoints.Add(searchPoint);
        return searchPoint;
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(lastSeenPosition, Vector3.one * .5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(lastSeenPosition, searchRadius);
        Gizmos.color = Color.green;
        for(int i = 0; i < historicPoints.Count; i++)
        {
            Gizmos.DrawWireCube(historicPoints[i], Vector3.one * .1f);
        }
    }





}
