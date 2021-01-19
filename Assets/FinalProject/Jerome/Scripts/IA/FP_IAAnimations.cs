using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_IAAnimations : MonoBehaviour
{
    [SerializeField] Animator mecanim = null;
    [SerializeField] string walkParameter = "walk";
    [SerializeField] string hitParameter = "hit";

    public bool IsValid => mecanim;



    public void SetWalkAnimation(bool _state)
    {
        //wall = _state
    }
    public void SetHitAnimation(bool _state)
    {
        //hit = _state
    }
}
