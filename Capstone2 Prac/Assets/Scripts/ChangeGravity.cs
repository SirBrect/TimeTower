using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    public float gravOffsetX = .5f;
    public float gravOffsetY = .5f;
    public Vector3 gravityVec = new Vector3 (0f, -9.8f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.gravity = new Vector3(-1f, 0f, 0f);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //if (gravityVec.x < 20f){
            //    gravityVec.x += gravOffsetX;
            //}
            //if (gravityVec.y < 20f){
            //    gravityVec.y += gravOffsetY;
            //}
            //Physics.gravity = gravityVec;
            transform.Rotate(0,0,0.5f);
            //Physics.gravity = -1f * transform.up * Physics.gravity.magnitude;
            Debug.Log("Pos");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //if (gravityVec.x > -20f){
            //    gravityVec.x -= gravOffsetX;
            //}
            //if (gravityVec.y > -20f){
            //    gravityVec.y -= gravOffsetY;
            //}
            //Physics.gravity = gravityVec;
            transform.Rotate(0, 0, -0.5f);
            //Physics.gravity = -1f * transform.up * Physics.gravity.magnitude;
            Debug.Log("Neg");
        }
        Physics.gravity = -1f * transform.up.normalized * Physics.gravity.magnitude;
        Debug.DrawRay(transform.position, Physics.gravity,Color.blue);
    }

}
