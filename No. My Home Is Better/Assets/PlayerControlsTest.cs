using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsTest : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private float xVal;
    
    [SerializeField]
    private float height;

    [SerializeField]
    private Rigidbody2D playerBody;

    // Update is called once per frame
    void FixedUpdate()
    {
        float xVal = Input.GetAxis("Horizontal");
        
        playerBody.velocity = new Vector2(xVal * speed, playerBody.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, height);
        }

    }
}
