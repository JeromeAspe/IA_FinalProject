using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Node
{
    public Vector3 Position { get; private set; } = Vector3.zero;
    public bool IsNaviguable { get; private set; } = true;
    public float F => G + H;
    public float G { get; set; } = float.MaxValue; // n => start
    public float H { get; set; } = float.MaxValue; //heuristic => node => end

    public FP_Node Predecessor { get; set; } = null;

    public List<FP_Node> Successors { get; set; } = new List<FP_Node>();

    public FP_Node(float _x, float _y, float _z)
    {
        Position = new Vector3(_x, _y, _z);
    }
    public void AddSuccessor(FP_Node _successor) => Successors.Add(_successor);
    public void ResetCost()
    {
        G = float.MaxValue;
        H = float.MaxValue;
    }
    public void SetNaviguable(bool _status)
    {
        IsNaviguable = _status;
    }
}
