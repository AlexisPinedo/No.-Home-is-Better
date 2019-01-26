using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_place : MonoBehaviour
{
	public GameObject block;
	public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0)) {
			Vector3 pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			pos[2] = 0;
			Object.Instantiate(block, pos, Quaternion.identity);
			Debug.Log(Input.mousePosition);
	   }
    }
}
