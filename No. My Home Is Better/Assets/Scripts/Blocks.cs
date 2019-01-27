using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*Changing collision when blocks are placed above
         * 
         * 
         * 
         * */
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        /*If colliding with a dropped block, while this is also a dropped block, and the other block is above, disable collision
         * Need to change sprite as well
         */
        Debug.Log("Collider Tag: " + collision.collider.tag + "; Collider name: " + collision.collider.name + "; this.name: " + this.name + "; Position: " + collision.collider.transform.position.y.ToString());

        if (collision.collider.CompareTag("Block") && this.name == "Dropped Block" && collision.collider.name == "Dropped Block" && collision.collider.transform.position.y > this.transform.position.y + 0.1f)
        {
            this.GetComponent<Collider2D>().enabled = false;
            //collision.collider.enabled = false;
        }
    }

}