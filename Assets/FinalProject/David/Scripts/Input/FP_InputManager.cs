using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class FP_InputManager : FP_Singleton<FP_InputManager>
{
    #region Event
    event Action UpdateInput = null;
    #endregion Event

    #region F/P
    [SerializeField, Header("All game axis")] List<FP_Axis> axis = new List<FP_Axis>();
    [SerializeField, Header("All game button")] List<FP_Button> buttons = new List<FP_Button>();


    public static Vector3 MousePosition => Input.mousePosition;
    #endregion F/P

    #region Unity Methods
    public override void Awake() => base.Awake();

    private void Update() => UpdateInput?.Invoke();

    private void OnDestroy() => UpdateInput = null;

    #endregion Unity Methods

    #region Custom Methods

    public void RegisterAxis(AxisAction _action, Action<float> _event)
    {
        List<FP_Axis> _axis = axis.Where(a => a.AxisAction == _action).ToList();
        _axis.ForEach(a => UpdateInput += () => _event?.Invoke(a.InputAction));

    }
    public void UnRegisterAxis(AxisAction _action, Action<float> _event)
    {
        List<FP_Axis> _axis = axis.Where(a => a.AxisAction == _action).ToList();
        _axis.ForEach(a => UpdateInput -= () => _event?.Invoke(a.InputAction));

    }
    public void RegisterButton(ButtonAction _action, Action<bool> _event)
    {
        List<FP_Button> _buttons = buttons.Where(b => b.ButtonAction == _action).ToList();
        _buttons.ForEach(b => UpdateInput += () => _event?.Invoke(b.InputAction));

    }
    public void UnRegisterButton(ButtonAction _action, Action<bool> _event)
    {
        List<FP_Button> _buttons = buttons.Where(b => b.ButtonAction == _action).ToList();
        _buttons.ForEach(b => UpdateInput -= () => _event?.Invoke(b.InputAction));

    }


    public void CreateAxis()
    {
        FP_Axis _newAxis = new FP_Axis();
        axis.Add(_newAxis);
    }
    public void CreateButton()
    {
        FP_Button _newButton = new FP_Button();
        buttons.Add(_newButton);
    }

    public void DeleteAllAxis()
    {
        axis.Clear();
    }
    public void DeleteAxisAt(int _index)
    {
        axis.RemoveAt(_index);
    }
    public void DeleteAllButtons()
    {
        buttons.Clear();
    }
    public void DeleteButtonAt(int _index)
    {
        buttons.RemoveAt(_index);
    }
    #endregion Custom Methods
}
