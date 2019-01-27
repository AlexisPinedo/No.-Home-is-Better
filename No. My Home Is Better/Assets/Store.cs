using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField]
    private GameObject block;

    private GameObject playerBlock;

    private PlayerControlsTest collidedPlayer;

    private bool stay = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        collidedPlayer = other.gameObject.GetComponent<PlayerControlsTest>();
        if (collidedPlayer.blockGrabbed == false)
        {
            Instantiate(block, new Vector3(other.gameObject.transform.position.x + .5f, other.gameObject.transform.position.y, other.gameObject.transform.position.z), Quaternion.identity);
            playerBlock = GameObject.Find("Block(Clone)");
            playerBlock.transform.parent = collidedPlayer.transform;
            collidedPlayer.blockGrabbed = true;
        }
    }

}