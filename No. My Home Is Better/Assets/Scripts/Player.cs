using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float height;

    [SerializeField]
    private Rigidbody2D playerBody;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private GameObject Cursor;

    [SerializeField]
    private float rayCastLength = 0.2f;

    [SerializeField]
    private block_place BlockController;

    [SerializeField]
    private int controllerNumber;

    private GameObject grabbedBlock;

    private float xVal;

    private bool isGrounded = true;

    private bool blockGrabbed = false;

    private string horizontalAxis;

    private string jumpButton;

    private string grabButton;

    private GameObject CurrentCursor = null;
    


    private void Awake()
    {
        SetControllerNumber();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xVal = Input.GetAxis("Horizontal");
        bool facingLeft = xVal <= 0 ? true : false;

        //CreateCursor
        CreateCursor(facingLeft);

        //Detect current block for grabbing

        //Place block

  

        playerBody.velocity = new Vector2(xVal * speed, playerBody.velocity.y);

        if (Input.GetButtonDown(jumpButton) && isGrounded)
        {
            Debug.Log("trying to jump");
            playerBody.velocity = new Vector2(playerBody.velocity.x, height);
            isGrounded = false;
        }
    }

    /*Determine left or right with ternary statement
     * returns true if left, false if right;
     * */
    //private bool FacingLeft(float xVal)
    //{
    //    return xVal <= 0 ? true : false;
    //}

    private void GrabBlock(Collider2D collider)
    {
        collider.gameObject.transform.parent = playerGameObject.transform;
        blockGrabbed = true;
    }

    /*Create Cursor
     * Use block_controller game object (grid)
     * GetValidSlotsLR - method returns the cursor position left and right of the player
     * Instantiate Cursor at position, Destroy when another is created 
     */
    void CreateCursor(bool facingLeft)
    {
        BlockController.PlaceBlock(Vector3.zero);

        ////Get the free positions
        //List<Vector3> freePosition = BlockController.GetValidSlotsLR(this.transform.position);
        //Vector3 cursorPosition = facingLeft ? freePosition[0] : freePosition[1];
        //if (cursorPosition != BlockController.DNE)
        //{
        //    if(!CurrentCursor)
        //    {
        //        CurrentCursor = Instantiate(Cursor, cursorPosition, Quaternion.identity);
        //    }
        //    else if(CurrentCursor.transform.position != cursorPosition)
        //    {
        //        Destroy(CurrentCursor);
        //        CurrentCursor = Instantiate(Cursor, cursorPosition, Quaternion.identity);
        //    }
        //}
        


    }

    private void DestroyCursor(Collider2D collider)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collision detected");
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Block"))
        {
            //Debug.Log("Player hit ground");
            isGrounded = true;
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
