using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FP_Astar
{


    #region Events
    public Action<FP_Path> OnPathCompleted = null;
    #endregion

    #region Fields/Properties
    #endregion

    #region Unity Methods
    #endregion

    #region Others Methods
    public void AskForPath(FP_Node _start, FP_Node _end)
    {
        FP_Grid.Instance?.ResetGridCost();
        List<FP_Node> _openList = new List<FP_Node>(), _closeList = new List<FP_Node>();
        _start.G = 0;
        _openList.Add(_start);
        while (_openList.Count != 0)
        {
            FP_Node _current = _openList[0];
            if (_current == _end)
            {
                FP_Path _path = new FP_Path(_start, _end);
                OnPathCompleted?.Invoke(_path);
                return;
            }
            _openList.Remove(_current);
            _closeList.Add(_current);
            for (int i = 0; i < _current.Successors.Count; i++)
            {
                FP_Node _successor = _current.Successors[i];
                float _g = _current.G + Vector3.Distance(_current.Position, _current.Successors[i].Position);
                if (_g < _successor.G)
                {
                    _successor.Predecessor = _current;
                    _successor.G = _g;
                    _successor.H = Vector3.Distance(_successor.Position, _end.Position);
                    if (!_openList.Contains(_successor) && _successor.IsNaviguable)
                        _openList.Add(_successor);
                }
            }
        }
    }
    #endregion
}

public class FP_Path
{
    public List<FP_Node> FinalPath { get; private set; } = new List<FP_Node>();
    public FP_Path(FP_Node _startNode, FP_Node _endNode)
    {
        FinalPath = GetFinalPath(_startNode, _endNode);
    }
    List<FP_Node> GetFinalPath(FP_Node _startNode, FP_Node _endNode)
    {
        List<FP_Node> _path = new List<FP_Node>();
        FP_Node _current = _endNode;
        _path.Add(_current);
        while (_current != _startNode)
        {
            _current = _current.Predecessor;
            _path.Add(_current);
        }
        _path.Reverse();
        return _path;
    }
    public void DrawPath()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < FinalPath.Count; i++)
        {
            if (i < FinalPath.Count - 1)
                Gizmos.DrawLine(FinalPath[i].Position + Vector3.up, FinalPath[i + 1].Position + Vector3.up);
            Gizmos.DrawSphere(FinalPath[i].Position + Vector3.up, .2f);
            Gizmos.DrawLine(FinalPath[i].Position + Vector3.up, FinalPath[i].Position);
        }
    }
}