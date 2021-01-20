using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_CoverBehaviour : MonoBehaviour
{
    Dictionary<int, FP_Obstacle> covers = new Dictionary<int, FP_Obstacle>();
    [SerializeField] Vector3 target = Vector3.zero;

    public bool HasTarget { get; set; } = false;
    private void Update()
    {
        Debug.Log(covers.Count);
    }
    public void SetTarget(Vector3 _target)
    {
        target = _target;
        HasTarget = true;
    }
    public Vector3 GetTarget() => target;
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
    public Vector3 GetBestCover()
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
        _cover.SetTarget(target);
        return _cover.GetBestCoverSide().transform.position;
    }
}
