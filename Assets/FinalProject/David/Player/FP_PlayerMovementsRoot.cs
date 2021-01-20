using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PlayerMovementsRoot : MonoBehaviour
{
    [SerializeField] Animator mecanim = null;
    [SerializeField, Header("Movements Speed"), Range(0, 200)] float movementSpeed = 5;
    [SerializeField, Header("Rotate Speed"), Range(0, 500)] float rotateSpeed = 10;
    [SerializeField] string verticalParam = "Vertical";
    [SerializeField] string horizontalParam = "Horizontal";
    [SerializeField] string shootParam = "shoot";
    [SerializeField, Header("Vertical value")] float verticalValue = 0f;
    [SerializeField, Header("Horizontal value")] float horizontalValue = 0f;
    public bool IsValid => mecanim;
    private void Start()
    {
        InitMovements();
    }
    private void Update()
    {
        UpdateMovements();
    }
    private void OnDestroy()
    {
        FP_InputManager.Instance?.UnRegisterAxis(AxisAction.VerticalMove, UpdateVerticalValue);
        FP_InputManager.Instance.UnRegisterAxis(AxisAction.MouseX, UpdateHorizontalValue);
    }
    void UpdateMovements()
    {
        if (!IsValid) return;
        mecanim.SetFloat(verticalParam, verticalValue,.2f, Time.deltaTime );
        mecanim.SetFloat(horizontalParam, horizontalValue, .2f, Time.deltaTime );
        
    }
  
    public void UpdateVerticalValue(float _value)
    {
        verticalValue = _value * movementSpeed;
    }
    public void UpdateHorizontalValue(float _value)
    {
        horizontalValue = _value * rotateSpeed;

    }
    void InitMovements()
    {
        FP_InputManager.Instance?.RegisterAxis(AxisAction.VerticalMove, UpdateVerticalValue);
        FP_InputManager.Instance.RegisterAxis(AxisAction.MouseX, UpdateHorizontalValue);
    }
}
