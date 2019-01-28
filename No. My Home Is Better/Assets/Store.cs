using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private GameObject block;

    private GameObject playerBlock;

    private Player collidedPlayer;

    private bool stay = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            collidedPlayer = other.gameObject.GetComponent<Player>();
            if (collidedPlayer.blockGrabbed == false)
            {
                Instantiate(block, new Vector3(other.transform.position.x + .5f, other.transform.position.y + 0.1f, other.transform.position.z), Quaternion.identity);
                playerBlock = GameObject.Find("Block(Clone)");
                playerBlock.transform.parent = collidedPlayer.transform;
                collidedPlayer.blockGrabbed = true;
            }
        }
    }

}