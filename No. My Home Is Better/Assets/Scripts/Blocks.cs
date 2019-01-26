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
        //Instantiation of cursor blocks
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D [] contacts = new ContactPoint2D[20];
        collision.GetContacts(contacts);
        Debug.Log(collision.contacts);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var item in collision.contacts)
        {
            Debug.DrawLine(transform.position, collision.transform.position);

        }
    }
}
