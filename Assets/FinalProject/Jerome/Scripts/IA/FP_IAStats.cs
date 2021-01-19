using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FP_IAStats 
{
    public event Action<float> OnPanicUpdate = null;
    public event Action OnResetStats = null;
    [SerializeField] int objectiveAttempts = 0, totalRewards = 0, totalFails=0;
    [SerializeField, Range(0, 100)] float attemptPercentReset = 20,globalProgress = 0,currentFailProgress = 0;
    //[SerializeField] float globalPanicLevel = 1;
    //[SerializeField] bool usePanicLevel = false;

    public float GlobalProgressRatio => (float)totalRewards / totalFails;

    public float IAFailProgress => (objectiveAttempts / 50f) * 100;
    public float PanicValue => 1 + (IAFailProgress / 50);

    public bool IANeedReset => IAFailProgress > attemptPercentReset;

    public void AddReward(int _reward)
    {
        totalRewards += _reward;
        globalProgress = GlobalProgressRatio;
    }
    public void AddFail(int _fail)
    {
        totalFails += _fail;
        globalProgress = GlobalProgressRatio;
    }
    public void AddAttempt(int _objectiveAttempts)
    {
        objectiveAttempts += _objectiveAttempts;
        currentFailProgress = IAFailProgress;
        /*
         * if(usePanicLevel)
         * 
         * globalPanicLevel = PanicValue;
         * OnPanicUpdate invoke (globalPanicLevel)
         */

    }

    public void ResetStats()
    {
        objectiveAttempts = 0;
        //globalPanicLevel = 1;

        //OnPanicUpdate invoke (globalPanicLevel)
        currentFailProgress = IAFailProgress;
        OnResetStats?.Invoke();
    }
}
