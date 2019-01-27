using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    [SerializeField]
    public Player player;

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
        if(player.BlockController.IsBlockAbove(transform.position))
        {
            GetComponent<Collider2D>().enabled = false;
        }

    }

}
