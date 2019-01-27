using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float height = 7f;

    [SerializeField]
    private float gravitySpeed = -20f;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private GameObject Cursor;

    [SerializeField]
    private block_place BlockController;

    [SerializeField]
    private int controllerNumber = 1;

    private Rigidbody2D playerBody;

    private GameObject grabbedBlock;

    private float xVal;

    private bool isGrounded = true;

    private string horizontalAxis;

    private string jumpButton;

    private string grabButton;

    private GameObject CurrentCursor = null;

    public GameObject droppedBlock;

    public bool blockGrabbed = false;

	private bool facingLeft = false;
    private void Awake()
    {
        SetControllerNumber();
        playerBody = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*Get the horizontal movement of the player to determine if facing left or right
         * Increase the gravity for better movement
         * Set the player's velocity and handle jump
         */
        float xVal = Input.GetAxis(horizontalAxis);
        
        if(xVal < 0)
        {
			facingLeft = true;
		}
		else if(xVal > 0)
		{
			facingLeft = false;
		}
        Physics2D.gravity = new Vector2(0, gravitySpeed);

        playerBody.velocity = new Vector2(xVal * speed, playerBody.velocity.y);

        if (Input.GetButtonDown(jumpButton) && isGrounded)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, height);
            isGrounded = false;
        }

        //CreateCursor if the player is holding a block
        if (blockGrabbed)
        {
            CreateCursor(facingLeft);
            //Place block
            if(Input.GetButtonDown(grabButton))
            {
                PlaceBlock();
            }
        }

        //Place block




    }

    /*Determine left or right with ternary statement
     * returns true if left, false if right;
     * */
    //private bool FacingLeft(float xVal)
    //{
    //    return xVal <= 0 ? true : false;
    //}

    private void PlaceBlock()
    {
        droppedBlock = GameObject.Find("Block(Clone)");
        //playerGameObject.transform.Find("Block").parent = null;
        droppedBlock.transform.parent = null;
        droppedBlock.GetComponent<Collider2D>().enabled = true;

        droppedBlock.transform.position = CurrentCursor.transform.position;
        BlockController.PlaceBlock(droppedBlock.transform.position, droppedBlock);
        Destroy(CurrentCursor);

        droppedBlock.name = "Dropped Block";
        blockGrabbed = false;
    }

    /*Create Cursor
     * Use block_controller game object (grid)
     * GetValidSlotsLR - method returns the cursor position left and right of the player
     * Instantiate Cursor at position, Destroy when another is created 
     */
    void CreateCursor(bool facingLeft)
    {
        //BlockController.PlaceBlock(Vector3.zero);

        //Get the free positions
        List<Vector3> freePosition = BlockController.GetValidSlotsLR(this.transform.position);
        if (freePosition != null)
        {
            Vector3 cursorPosition = facingLeft ? freePosition[0] : freePosition[1];
            if (cursorPosition != BlockController.DNE)
            {
                if (!CurrentCursor)
                {
                    CurrentCursor = Instantiate(Cursor, cursorPosition, Quaternion.identity);
                }
                else if (CurrentCursor.transform.position != cursorPosition)
                {
                    Destroy(CurrentCursor);
                    CurrentCursor = Instantiate(Cursor, cursorPosition, Quaternion.identity);
                }

                //BlockController.PlaceBlock(cursorPosition);
            }
        }



    }


    //Keep player grounded. Jump resets on the ground and on top of a block.
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collision detected");
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Block"))
        {
            //Debug.Log("Player hit ground");
            isGrounded = true;
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Trigger detected");
        if (other.gameObject.CompareTag("Block") && Input.GetButton(grabButton) && blockGrabbed == false)
        {
            Debug.Log("Grabbing Block");
            other.gameObject.transform.parent = playerGameObject.transform;
            blockGrabbed = true;
        }
    }

    //Movement
    private void SetControllerNumber()
    {

        horizontalAxis = "J" + controllerNumber + "Horizontal";
        jumpButton = "J" + controllerNumber + "Jump";
        grabButton = "J" + controllerNumber + "Grab";
        Debug.Log(horizontalAxis + " " + jumpButton + " " + grabButton);
        Debug.Log(horizontalAxis + jumpButton);
    }
}
