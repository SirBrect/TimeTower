using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAroundPoint : MonoBehaviour
{
    public GameObject rotPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space")){
            transform.RotateAround(rotPoint.transform.position, new Vector3(-1f, 0f, 0f), .5f);
        }
    }
}
