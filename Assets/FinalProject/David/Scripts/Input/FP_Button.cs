using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FP_Button : FP_Input<bool>
{
    [SerializeField, Header("Button keycode - cf : Unity KeyCode")] KeyCode buttonKeyCode = new KeyCode();
    [SerializeField, Header("Button state")] ButtonState buttonState = ButtonState.Down;
    [SerializeField, Header("Button action")] ButtonAction buttonAction = ButtonAction.None;
    [SerializeField, Header("Button Value")] bool buttonValue = false;


    public ButtonAction ButtonAction => buttonAction;
    public override bool InputAction
    {
        get
        {
            if (buttonState == ButtonState.Down)
                return buttonValue =  Input.GetKeyDown(buttonKeyCode);
            else if (buttonState == ButtonState.Pressed)
                return buttonValue =   Input.GetKey(buttonKeyCode);
            else return buttonValue =  Input.GetKeyUp(buttonKeyCode);
        }
    }
  

    public override bool InputFeedBack => buttonValue;

    enum ButtonState
    {
        Down,
        Pressed,
        Up
    }
}
