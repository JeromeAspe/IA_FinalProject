using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FP_CameraManager : FP_Singleton<FP_CameraManager>, IHandler<string, FP_CameraBehaviour>
{
    public static event Action OnUpdateManager = null;

    #region F/P
    Dictionary<string, FP_CameraBehaviour> allCameras = new Dictionary<string, FP_CameraBehaviour>();
    public Dictionary<string, FP_CameraBehaviour> Handler => allCameras;
    #endregion F/P
    #region Unity Methods
    public override void Awake()
    {
        base.Awake();
    }

    private void LateUpdate() => OnUpdateManager?.Invoke();

    #endregion Unity Methods
    #region IHandler Interface
    public void Add(FP_CameraBehaviour _camera)
    {
        if (!IsValid || !_camera.IsValid) return;
        if (Exists(_camera))
            throw new Exception($"{GetType().Name} error => {_camera.ID} already exists");
        allCameras.Add(_camera.ID, _camera);
        _camera.name += " [MANAGED]";
        OnUpdateManager += _camera.OnUpdateCameraBehaviour;
    }

    public void Disable(FP_CameraBehaviour _camera)
    {
        if (!Exists(_camera.ID)) return;
        Get(_camera.ID).Disable();
    }

    public void Disable(string _cameraID)
    {
        if (!Exists(_cameraID)) return;
        Get(_cameraID).Disable();
    }

    public void Enable(FP_CameraBehaviour _camera)
    {
        if (!Exists(_camera.ID)) return;
        foreach (KeyValuePair<string, FP_CameraBehaviour> cameras in allCameras)
            cameras.Value.Disable();
        Get(_camera.ID).Enable();
    }

    public void Enable(string _cameraID)
    {
        if (!Exists(_cameraID)) return;
        foreach (KeyValuePair<string, FP_CameraBehaviour> cameras in allCameras)
            cameras.Value.Disable();
        Get(_cameraID).Enable();
    }

    public bool Exists(FP_CameraBehaviour _camera)
    {
        bool _exist = allCameras.ContainsKey(_camera.ID);
        if (!_exist)
            Debug.Log($" error => {_camera.ID} does not exists!");
        return _exist;
    }

    public bool Exists(string _cameraID)
    {
        bool _exist = allCameras.ContainsKey(_cameraID);
        if (!_exist)
            Debug.Log($" error => {_cameraID} does not exists!");
        return _exist;
    }

    public FP_CameraBehaviour Get(string _cameraID)
    {
        if (!Exists(_cameraID))
            return null;

        return allCameras[_cameraID];
    }

    public void Remove(FP_CameraBehaviour _camera)
    {
        if (!IsValid || !_camera.IsValid) return;
        if (!Exists(_camera.ID)) return;
        OnUpdateManager -= Get(_camera.ID).OnUpdateCameraBehaviour;
        allCameras.Remove(_camera.ID);
    }

    public void Remove(string _cameraID)
    {
        if (!IsValid || !Get(_cameraID).IsValid) return;
        if (!Exists(_cameraID)) return;
        OnUpdateManager -= Get(_cameraID).OnUpdateCameraBehaviour;
        allCameras.Remove(_cameraID);
    }
    #endregion IHandler Interface

}
