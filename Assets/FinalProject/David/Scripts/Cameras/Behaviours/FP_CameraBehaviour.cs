using UnityEngine;
using System;

public abstract class FP_CameraBehaviour : MonoBehaviour,IHandledItem<string>
{
    #region Events
    public Action OnUpdateCameraBehaviour = null;

    #endregion Events
    #region F/P
    [SerializeField, Header("Camera ID")] string cameraID = "camera ID";
    [SerializeField, Header("Camera Settings")] protected FP_CameraSettings settings = new FP_CameraSettings();
    [SerializeField, Header("Camera Visual Debug")] FP_CameraVisualDebug visualDebug = new FP_CameraVisualDebug();
    [SerializeField, Header("Camera Component")] Camera cameraComponent = null;

    public Vector3 CameraFinalPosition => settings.TargetPosition + settings.Offset;
    public Vector3 CameraPosition => transform.position;

    public FP_CameraSettings Settings => settings;
    public virtual bool IsValid => cameraComponent;

    #endregion F/P

    #region Unity Methods
    protected virtual void Start() => InitCamera();
    protected virtual void OnDestroy()
    {
        RemoveHandledItem();
        OnUpdateCameraBehaviour = null;
    }
    public virtual void OnDrawGizmos() => visualDebug.DrawVisualDebug(settings, CameraPosition, CameraFinalPosition);



    #endregion Unity Methods

    #region Custom Methods
    public virtual void InitCamera()
    {
        if (!cameraComponent) cameraComponent = GetComponent<Camera>();
        if (!cameraComponent) cameraComponent = gameObject.AddComponent<Camera>(); //Methode d'extension A FAIRE
        InitHandledItem();
    }
    protected virtual void SmoothLookAt() { }
    protected virtual void SmoothFollow() { }

    public void SetID(string _id) => cameraID = _id;

    #endregion Custom Methods

    #region Interface IHandledItem
    public string ID => cameraID;

    public void Disable()
    {
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    public void InitHandledItem() => FP_CameraManager.Instance?.Add(this);
    
    public void RemoveHandledItem() => FP_CameraManager.Instance?.Remove(this);
    
    #endregion Interface IHandledItem

}
