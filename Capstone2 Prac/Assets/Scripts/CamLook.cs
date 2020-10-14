using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLook : MonoBehaviour
{
    float horizontal;
    float vertical;
    public float maxDist = 0.5f;
    float movementSpeed = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("CamLookVert");
        horizontal = Input.GetAxis("CamLookHorizontal");
        Vector3 moveDir = new Vector3(vertical, horizontal, 0f);
        transform.Translate(moveDir * Time.deltaTime * movementSpeed);
    }
}
