using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_CoverBehaviour : MonoBehaviour
{
    Dictionary<int, FP_Obstacle> covers = new Dictionary<int, FP_Obstacle>();





    public void Add(FP_Obstacle _obstacle)
    {
        if (Exists(_obstacle.ID)) return;
        covers.Add(_obstacle.ID, _obstacle);
    }
    public bool Exists(int _id)
    {
        return covers.ContainsKey(_id);
    }
    public bool HasCover()
    {
        return covers.Count != 0;
    }
    public Vector3 GetBestCover(Vector3 _target)
    {
        float _minDistance = int.MaxValue;
        FP_Obstacle _cover = null;
        foreach(KeyValuePair<int,FP_Obstacle> _obstacle in covers)
        {
            float _distance = Vector3.Distance(transform.position, _obstacle.Value.transform.position);
            if ( _distance < _minDistance)
            {
                _minDistance = _distance;
                _cover = _obstacle.Value;
            }
        }
        return _cover.GetBestCoverSide().transform.position;
    }
}
