using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PlayerRootMovements : MonoBehaviour
{
    [SerializeField] Animator mecanim = null;
    [SerializeField, Range(0, 360)] float clampYRotation = 40;
    [SerializeField, Header("Movement Speed"), Range(0, 200)] float movementSpeed = 5;
    [SerializeField, Header("Rotate Speed"), Range(0, 500)] float rotateSpeed = 100;
    [SerializeField] string horizontalParam = "Horizontal";
    [SerializeField] string verticalParam = "Vertical";
    [SerializeField] string shootParam = "shoot";
    float horizontal = 0, vertical = 0,rotateVertical =0 , rotateHorizontal =0 ;
    bool isActive = true;

    public bool IsOnMove => (vertical != 0 || horizontal != 0);
    public bool IsValid => mecanim;
    private void Start()
    {
        InitMovements();
    }
    private void Update()
    {
        UpdateValues();
        SetAnimatorValues();
    }
    private void OnDestroy()
    {
        FP_InputManager.Instance.UnRegisterAxis(AxisAction.VerticalMove, SetVertical);
        FP_InputManager.Instance.UnRegisterAxis(AxisAction.HorizontalMove, SetHorizontal);
        //FP_InputManager.Instance.UnRegisterAxis(AxisAction.MouseX, SetHorizontal);
        FP_InputManager.Instance.UnRegisterAxis(AxisAction.MouseX, SetRotateHorizontal);
        FP_InputManager.Instance.UnRegisterAxis(AxisAction.MouseY, SetRotateVertical);
        //FP_InputManager.Instance.UnRegisterAxis(AxisAction.HorizontalAxis, SetRotateHorizontal);
        //FP_InputManager.Instance.UnRegisterAxis(AxisAction.VerticalAxis, SetRotateVertical);
    }
    void InitMovements()
    {
        if (!mecanim) mecanim = GetComponent<Animator>();
        FP_InputManager.Instance.RegisterAxis(AxisAction.VerticalMove, SetVertical);
        FP_InputManager.Instance.RegisterAxis(AxisAction.HorizontalMove, SetHorizontal);
        //FP_InputManager.Instance.RegisterAxis(AxisAction.MouseX, SetHorizontal);
        FP_InputManager.Instance.RegisterAxis(AxisAction.MouseX, SetRotateHorizontal);
        FP_InputManager.Instance.RegisterAxis(AxisAction.MouseY, SetRotateVertical);
        //FP_InputManager.Instance.RegisterAxis(AxisAction.HorizontalAxis, SetRotateHorizontal);
        //FP_InputManager.Instance.RegisterAxis(AxisAction.VerticalAxis, SetRotateVertical);
    }
    public void UpdateValues()
    {
        if (!isActive) return;
        UpdateRotation();
    }
    void UpdateRotation()
    {
        transform.eulerAngles = new Vector3(GetClampedValue(rotateVertical, clampYRotation), transform.eulerAngles.y + rotateHorizontal, 0);
        
    }

    public void SetAnimatorValues()
    {

        mecanim.SetFloat(verticalParam, vertical, 0.2f, Time.deltaTime);
        mecanim.SetFloat(horizontalParam, horizontal, 0.2f, Time.deltaTime);
    }
    public void SetVertical(float _value)
    {
        if (!isActive) return;
        vertical = _value * movementSpeed;
    }

    public void SetHorizontal(float _value)
    {
        if (!isActive) return;
        horizontal = _value * rotateSpeed;
    }
    public void SetRotateVertical(float _value)
    {
        if (!isActive) return;
        rotateVertical += _value;
        rotateVertical %= 360;
    }
    public void SetRotateHorizontal(float _value)
    {
        if (!isActive) return;
        rotateHorizontal = _value;
    }
    public float GetClampedValue(float _value, float _clamp)
    {

        if (_value > _clamp)
            return rotateVertical = _clamp;
        else if (_value < -_clamp)
            return rotateVertical = -_clamp;
        return _value;
    }
    public void Disable()
    {
        isActive = false;
        horizontal = 0;
        vertical = 0;

    }
    public void Enable()
    {
        isActive = true;
    }
}
