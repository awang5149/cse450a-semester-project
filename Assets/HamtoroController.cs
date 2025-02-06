using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamtoroController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // move up
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 0.2f, 0);
        }

        // move down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -0.2f, 0);
        }


        // move left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.2f, 0, 0);
        }


        // move right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.2f, 0, 0);
        }
    }
}
