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

    public float gravityAxisVal;

    public bool triggers = false;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("joystick button 3")){
            if (triggers){
                triggers = false;
            }
            else{
                triggers = true;
            }
        }
        if (triggers){
            gravityAxisVal = Input.GetAxis("Gravity");
            if (gravityAxisVal > 0.01f || gravityAxisVal < -0.01f)
            {
                Debug.Log(gravityAxisVal);
                transform.Rotate(0, 0, gravityAxisVal);
            }
        }
        else{
            gravityAxisVal = Input.GetAxis("GravAlt");
            if (gravityAxisVal > 0.01f || gravityAxisVal < -0.01f)
            {
                Debug.Log(gravityAxisVal);
                transform.Rotate(0, 0, gravityAxisVal);
            }
        }
        
        
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

        if (!dead && Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0,0,1.0f);
        }
        if (!dead && Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, -1.0f);
        }
        lastPos = Input.mousePosition;
        Physics.gravity = -1f * transform.up.normalized * Physics.gravity.magnitude;
        //Debug.DrawRay(transform.position, Physics.gravity,Color.blue);
    }

}
