using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // public CharacterController controller;
    public bool usingForces = false;
    public Rigidbody rb;
    public float speed = 1.0f;
    float horizontal = 0.0f;
    float prevHorizontal = 0.0f;
    bool jump = false;
    Vector3 movement = Vector3.zero;
    //public float stopDrag = 5f;
    [SerializeField]
    private bool grounded;
    //public float accelerateBoost = 2.0f;
    //public float accelerateSeconds = 0.1f;
    //[SerializeField]
    //float accelerate;
    //IEnumerator accelBoost;
    public float jumpPower;
    public float jumpStep;
    public float jumpTime;
    public float maxSpeed;
    public float speedUpStep;
    public float speedDownStep;
    [SerializeField]
    private float currentSpeed;
    float currentJumpTime;
    bool jumping;
    public float fallingSpeed = 2f;
    public Animator animator;
    float direction;    // 0 is left, 1 is right
    public GameObject model;
    //Quaternion right;
    //Quaternion left;

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        currentSpeed = 0.0f;
        jumping = false;
        //right = Quaternion.Euler(model.transform.forward);
        //left = Quaternion.Euler(-1*model.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(Mathf.Abs(horizontal) < 0.1f)
        {
            if(currentSpeed > 0f)
            {
                currentSpeed -= speedDownStep;
                currentSpeed = Mathf.Max(currentSpeed, 0f);
                direction = 1f;
                
            }
            else if(currentSpeed < 0f)
            {
                currentSpeed += speedDownStep;
                currentSpeed = Mathf.Min(currentSpeed, 0f);
                direction = 0f;
            }
        }
        else
        {
            if(horizontal > 0f)
            {
                currentSpeed += speedUpStep;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
                direction = 1f;
                //model.transform.rotation = Quaternion.Slerp(model.transform.rotation, right, Time.deltaTime * 5f);
                model.transform.forward = Vector3.Slerp(model.transform.forward, transform.right, 0.1f);
            }
            else
            {
                currentSpeed -= speedUpStep;
                currentSpeed = Mathf.Max(currentSpeed, -1f * maxSpeed);
                direction = 0f;
                //model.transform.rotation = Quaternion.Slerp(model.transform.rotation, left, Time.deltaTime * 5f);
                model.transform.forward = Vector3.Slerp(model.transform.forward, -1f*transform.right, 0.1f);
            }
        }
        movement = new Vector3(currentSpeed * Time.deltaTime, 0f, 0f);
        animator.SetFloat("Speed", Mathf.Abs(currentSpeed) / maxSpeed);
        animator.SetFloat("Direction", direction);
        //if (usingForces)
        //{
        //    Move(movement);
        //}
        //else
        //{
        //    transform.Translate(movement);
        //}
        transform.Translate(movement);
        

        // Jump checks
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            if (grounded)
            {
                Jump();
                Debug.Log("Pressed jump!");
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if(!grounded && jumping)
            {
                currentJumpTime += Time.deltaTime;
                if (currentJumpTime < jumpTime)
                {
                    ContinueJump();
                    Debug.Log("Holding jump!");
                }
            }
        }
        else
        {
            jumping = false;
        }

        if (rb.velocity.y < 0.0f && rb.mass == 1f)
        {
            Fall();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * fallingSpeed, ForceMode.VelocityChange);
    }

    //void Move(Vector3 move)
    //{
    //    rb.AddForce(move*10f, ForceMode.VelocityChange);
    //}

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        currentJumpTime = 0f;
        Debug.Log("Here!");
        jumping = true;
    }

    void ContinueJump()
    {
        rb.AddForce(Vector3.up * jumpStep, ForceMode.VelocityChange);
    }

    public void Ground()
    {
        grounded = true;
        animator.SetBool("Grounded", true);
        jumping = false;
        //rb.mass = 1f;
    }

    public void Unground()
    {
        grounded = false;
        animator.SetBool("Grounded", false);
    }

    public void Fall()
    {
        //rb.mass = fallingMass;
        rb.AddForce(Vector3.down * fallingSpeed, ForceMode.Acceleration);
        jumping = false;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    /*
    IEnumerator AccelrateBoost()
    {
        accelerate = accelerateBoost;
        yield return new WaitForSeconds(accelerateSeconds);
        accelerate = 1.0f;
    }
    */
}
