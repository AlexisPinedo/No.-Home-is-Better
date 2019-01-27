using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerControlsTest : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float height;

    [SerializeField]
    private Rigidbody2D playerBody;
    
    [SerializeField]
    private GameObject playerGameObject;
    
    private float xVal;

    private bool isGrounded = true;

    private bool blockGrabbed = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        float xVal = Input.GetAxis("Horizontal");
        
        playerBody.velocity = new Vector2(xVal * speed, playerBody.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, height);
            isGrounded = false;
        }

        if (blockGrabbed == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Trying to let go of block");
            playerGameObject.transform.Find("Block").parent = null;
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
        if (other.gameObject.CompareTag("Block") && Input.GetKeyDown(KeyCode.LeftShift) && blockGrabbed == false)
        {
            other.gameObject.transform.parent = playerGameObject.transform;
            blockGrabbed = true;
        }
    }
}
    
