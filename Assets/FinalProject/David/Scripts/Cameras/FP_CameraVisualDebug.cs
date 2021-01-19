using System;
using UnityEngine;

[Serializable]
public class FP_CameraVisualDebug
{
    [SerializeField, Header("Visual Debug Size"), Range(0, 100)] float visualDebugSize = 0.3f;

    [SerializeField, Header("Camera Position Color")] Color cameraPositionColor = Color.cyan;
    [SerializeField, Header("Camera Final Position Color")] Color cameraFinalPositionColor = Color.blue;
    [SerializeField, Header("Camera Target Link Color")] Color cameraTargetLinkColor = Color.green;
    
    public void DrawVisualDebug(FP_CameraSettings _settings,Vector3 _cameraPosition,Vector3 _finalCameraPosition)
    {
        Gizmos.color = cameraPositionColor;
        Gizmos.DrawWireCube(_cameraPosition, Vector3.one * visualDebugSize);
        Gizmos.color = cameraTargetLinkColor;
        Gizmos.DrawLine(_cameraPosition, _settings.TargetPosition);
        Gizmos.color = cameraFinalPositionColor;
        Gizmos.DrawWireCube(_finalCameraPosition, Vector3.one * visualDebugSize);
        Gizmos.DrawLine(_cameraPosition, _finalCameraPosition);
        
    }
    

}


