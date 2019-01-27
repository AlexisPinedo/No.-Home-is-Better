using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input.LowLevel;
using UnityEngine.Rendering;


public class PlayerInput : MonoBehaviour{

//    private PlayerScript player;
//    
    private string horizontalAxis;
//
    private string jumpButton;
//
    private string grabButton;
//
    private int controllerNumber;
//    
//    public float Horizontal { get; set; }
//    
//
//    private void FixedUpdate()
//    {
//        Debug.Log(horizontalAxis);
//        Horizontal = Input.GetAxis(horizontalAxis);
//        Debug.Log(Horizontal);
//    }
//
    public enum Button
    {
        A, B,
    }
//
    public bool ButtonIsDown(Button button)
    {
        switch (button)
        {
                case Button.A:
                    Debug.Log("Jump button pressed");
                    return Input.GetButton(jumpButton);
                case Button.B:
                    Debug.Log(("Grab button pressed"));
                    return Input.GetButton(grabButton);      
        }
        return false;
    }
//    
//
//
    internal void SetControllerNumber(int number)
    {
        controllerNumber = number;
        horizontalAxis = "J" + controllerNumber + "Horizontal";
        jumpButton = "J" + controllerNumber + "Jump";
        grabButton = "J" + controllerNumber + "Grab";
    }
    
}
