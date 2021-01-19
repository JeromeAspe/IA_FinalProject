using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_CameraFPSBehaviour : FP_CameraBehaviour
{
    public override void InitCamera()
    {
        OnUpdateCameraBehaviour += SmoothFollow;
        OnUpdateCameraBehaviour += SmoothLookAt;
        base.InitCamera();
    }
    protected override void SmoothFollow()
    {

        transform.localPosition = settings.TargetPosition + settings.Offset /*+ settings.Target.forward*/;
    }
    protected override void SmoothLookAt()
    {
        transform.localRotation = settings.Target.rotation;
    }
}
