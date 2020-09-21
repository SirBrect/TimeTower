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
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.RotateAround(rotPoint.transform.position, new Vector3(0f, 0f, -1f), .5f);
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            transform.RotateAround(rotPoint.transform.position, new Vector3(0f, 0f, 1f), .5f);
        }
    }
}
