using System;
using UnityEngine;

[Serializable]
public class FP_Axis : FP_Input<float>
{
    [SerializeField, Header("Axis name - cf : Unity Input Manager")] string axisName = "AxisName";
    [SerializeField, Header("Axis value"), Range(-1, 1)] float axisValue = 0;
    [SerializeField, Header("Axis action")] AxisAction action = AxisAction.None;
    [SerializeField] bool invertAxis = false;

    public AxisAction AxisAction => action;
    public override float InputAction
    {
        get
        {
            try
            {
                axisValue = Input.GetAxis(axisName);
                return invertAxis ? -axisValue : axisValue;
            }
            catch (ArgumentException _noAxis)
            {
                Debug.Log(_noAxis.Message);
                return 0;
            }
        }
    }
    public override float InputFeedBack => invertAxis ? -axisValue : axisValue;
}
