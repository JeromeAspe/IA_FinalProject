using System;
using UnityEngine;

public interface ITarget : IStats
{
    Vector3 TargetPosition { get; }
}