using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Physics.gravity = new Vector3(-4.0f, 1f, 0f);
        }
    }
}
