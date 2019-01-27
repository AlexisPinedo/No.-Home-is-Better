using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField]
    public GameObject Player;

    public Sprite notAPlatform;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*Changing collision when blocks are placed above
         * if(blockabove(current pos))
         *      disable collision
         *      this.GetComponent<SpriteRenderer>().sprite = notAPlatform;
         * 
         * */
         //Debug.Log(Player.GetComponent<Player>().BlockController);
         
        /*if(Player.GetComponent<Player>().BlockController.IsBlockAbove(transform.position))
        {
			Debug.Log("Above");
            GetComponent<Collider2D>().enabled = false;
        }*/

    }

}
