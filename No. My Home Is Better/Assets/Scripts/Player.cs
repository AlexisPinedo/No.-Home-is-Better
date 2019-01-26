using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Dictionary<GameObject, HashSet<GameObject>> cursors;
    public GameObject Cursor;



    // Start is called before the first frame update
    void Start()
    {
        cursors = new Dictionary<GameObject, HashSet<GameObject>>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

            cursors[collider.gameObject].Add(Instantiate(Cursor, collider.transform.position + Vector3.right * collider.bounds.size.y, Quaternion.identity));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when block enters player's trigger, instantiate the cursor
        Collider2D collider = collision.GetComponent<Collider2D>();
        if (collider.CompareTag("Block"))
        {
            CreateCursor(collider);
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
