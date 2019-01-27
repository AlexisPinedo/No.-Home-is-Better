using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Debug = UnityEngine.Debug;

public class PlayerControlsTest : MonoBehaviour
{

//    
//    public PlayerInput Input { get; private set; }
//    
//    public PlayerScript Player { get; private set; }
//    
//    public int PlayerNumber { get; private set; }
//    
//
//
//    public void SetPlayer(PlayerScript player)
//    {
//        this.Player = player;
//        PlayerNumber = player.playerNumber;
//        Input = player.GetComponent<PlayerInput>();
//
//    }
    
    
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float height;

    [SerializeField]
    private Rigidbody2D playerBody;
    
    [SerializeField]
    private GameObject playerGameObject;
    
    private void Awake()
    {
        SetControllerNumber();
    }
    
    private float xVal;

    private bool isGrounded = true;

    public bool blockGrabbed = false;
    
    private string horizontalAxis;

    private string jumpButton;

    private string grabButton;

    public GameObject droppedBlock;
    
    //public float Horizontal { get; set; }
    
    [SerializeField]
    private int controllerNumber;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float xVal = Input.GetAxis(horizontalAxis);
        Debug.Log(xVal);
        playerBody.velocity = new Vector2(xVal * speed, playerBody.velocity.y);
            
        if (Input.GetButtonDown(jumpButton) && isGrounded)
        {
            Debug.Log("trying to jump");
            playerBody.velocity = new Vector2(playerBody.velocity.x, height);
            isGrounded = false;
        }

        if (blockGrabbed == true && Input.GetButtonDown(grabButton))
        {
            Debug.Log("Trying to let go of block");
                
            droppedBlock = GameObject.Find("Block(Clone)");
            //playerGameObject.transform.Find("Block").parent = null;
            droppedBlock.transform.parent = null;
            droppedBlock.name = "Dropped Block";
            blockGrabbed = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collision detected");
        if (other.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("Player hit ground");
            isGrounded = true;
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Trigger detected");
        if (other.gameObject.CompareTag("Block") && Input.GetButtonDown(grabButton) && blockGrabbed == false)
        {
            Debug.Log("Grabbing Block");
            other.gameObject.transform.parent = playerGameObject.transform;
            blockGrabbed = true;
        }
    }
    private void SetControllerNumber()
    {
        horizontalAxis = "J" + controllerNumber + "Horizontal";
        jumpButton = "J" + controllerNumber + "Jump";
        grabButton = "J" + controllerNumber + "Grab";
        Debug.Log(horizontalAxis + " " + jumpButton + " " + grabButton);
        Debug.Log(horizontalAxis + jumpButton);
    }
}
    
