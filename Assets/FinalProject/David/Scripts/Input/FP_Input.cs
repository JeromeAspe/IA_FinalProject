using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FP_Input<T> 
{
    public abstract T InputAction { get; }

    public abstract T InputFeedBack { get; }
}
