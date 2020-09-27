using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    public Vector3 lastPos;
    bool left = false;
    public float gravOffsetX = .5f;
    public float gravOffsetY = .5f;
    public Vector3 gravityVec = new Vector3 (0f, -9.8f, 0f);
    public bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && Input.GetMouseButton(0)){
            Debug.Log(Input.mousePosition);
            if (Input.mousePosition.x == lastPos.x){
                if (left){
                    transform.Rotate(0, 0, -1.5f);
                }
                else{
                    transform.Rotate(0, 0, 1.5f);
                }
            }
            if (Input.mousePosition.x > lastPos.x){
                transform.Rotate(0, 0, 1.5f);
                left = false;
            }
            if (Input.mousePosition.x < lastPos.x){
                transform.Rotate(0, 0, -1.5f);
                left = true;
            }
        }
        //Physics.gravity = new Vector3(-1f, 0f, 0f);
        if (!dead && Input.GetKey(KeyCode.RightArrow))
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
        if (!dead && Input.GetKey(KeyCode.LeftArrow))
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
        lastPos = Input.mousePosition;
        Physics.gravity = -1f * transform.up.normalized * Physics.gravity.magnitude;
        Debug.DrawRay(transform.position, Physics.gravity,Color.blue);
    }

}
