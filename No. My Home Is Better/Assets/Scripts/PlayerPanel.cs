using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;


public class PlayerPanel : MonoBehaviour
{
   
    private bool HasControllerAssigned;
   
    private int controllerNumber = 1;

    //[SerializeField] 
    //private GameObject Player;
    
    [SerializeField]
    private PlayerInput Input;

    private string PlayerTextBoxNumber;

    private APlayer player;
    
    [SerializeField]
    private Text Player1TextBox;

    [SerializeField] private Canvas PlayerCanvas;

    private void Awake()
    {
        HasControllerAssigned = false;
    }


    public void Update()
    {
        Debug.Log("Running");
        if(UnityEngine.Input.GetButton("Jump") && !HasControllerAssigned)
        {
            Debug.Log("A button was pressed");
            AssignController(controllerNumber);
            controllerNumber++;
        }

        if (HasControllerAssigned && Input.ButtonIsDown(PlayerInput.Button.A))
        {
            Debug.Log("Setting panel off");
            PlayerCanvas.gameObject.SetActive(false);
        }
            
    }

//    public bool isReady()
//    {
//        if (!HasControllerAssigned)
//            return false;
//        return true;
//    }
//    
    public void AssignController(int controller)
    {
        Debug.Log("Setting player to controller");
        Input.SetControllerNumber(controller);
        HasControllerAssigned = true;
        Player1TextBox.text = "Ready";
    }
}
