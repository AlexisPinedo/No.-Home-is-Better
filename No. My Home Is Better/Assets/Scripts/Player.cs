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
    private BoxCollider2D bigTrigger;

    private float xVal;

    private bool isGrounded = true;

    private bool blockGrabbed = false;

    private Dictionary<GameObject, HashSet<GameObject>> cursors;



    // Start is called before the first frame update
    void Start()
    {
        cursors = new Dictionary<GameObject, HashSet<GameObject>>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bigTrigger.enabled = blockGrabbed;
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

    void CreateCursor(Collider2D collider)
    {
        //Raycast up, left, right and instantiate wherever there isn't a collision (excluding the player)
        RaycastHit2D hitInfoUp = Physics2D.Raycast(collider.transform.position, Vector2.up, 10f);
        Debug.DrawRay(collider.transform.position, Vector2.up*2f, Color.green, .5f);

        RaycastHit2D hitInfoRight = Physics2D.Raycast(collider.transform.position, Vector2.right, 10f);
        Debug.DrawRay(collider.transform.position, Vector2.right*2f, Color.green, .5f);

        RaycastHit2D hitInfoLeft = Physics2D.Raycast(collider.transform.position, Vector2.left, 10f);
        Debug.DrawRay(collider.transform.position, Vector2.left*2f, Color.green, .5f);


        if (!hitInfoUp || hitInfoUp.collider.CompareTag("Player")) 
        {
            if (!cursors.ContainsKey(collider.gameObject))
            {
                cursors[collider.gameObject] = new HashSet<GameObject>();
            }

            cursors[collider.gameObject].Add(Instantiate(Cursor, collider.transform.position + Vector3.up * collider.bounds.size.y, Quaternion.identity));
        }

        if (!hitInfoLeft || hitInfoLeft.collider.CompareTag("Player"))
        {
            if (!cursors.ContainsKey(collider.gameObject))
            {
                cursors[collider.gameObject] = new HashSet<GameObject>();
            }

            cursors[collider.gameObject].Add(Instantiate(Cursor, collider.transform.position + Vector3.left * collider.bounds.size.x, Quaternion.identity));
        }

        if (!hitInfoRight || hitInfoRight.collider.CompareTag("Player"))
        {
            if (!cursors.ContainsKey(collider.gameObject))
            {
                cursors[collider.gameObject] = new HashSet<GameObject>();
            }

            cursors[collider.gameObject].Add(Instantiate(Cursor, collider.transform.position + Vector3.right * collider.bounds.size.x, Quaternion.identity));
        }



    }

    private void DestroyCursor(Collider2D collider)
    {
        if(cursors.ContainsKey(collider.gameObject))
        {
            foreach (var cursor in cursors[collider.gameObject])
            {
                Destroy(cursor);
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when block enters player's trigger, instantiate the cursor
        Collider2D collider = collision.GetComponent<Collider2D>();
        if (collider.CompareTag("Block"))
        {
            CreateCursor(collider);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") && Input.GetKeyDown(KeyCode.LeftShift) && blockGrabbed == false)
        {
            other.gameObject.transform.parent = playerGameObject.transform;
            blockGrabbed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D collider = collision.GetComponent<Collider2D>();
        if (collider.CompareTag("Block"))
        {
            DestroyCursor(collider);
        }
    }
}
