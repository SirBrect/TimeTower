﻿using System.Collections;
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
    public float rotateSpeed;
    [SerializeField]
    private float currentSpeed;
    float currentJumpTime;
    bool jumping;
    public float fallingSpeed = 2f;
    public Animator animator;
    float direction;    // 0 is left, 1 is right
    public GameObject model;
    public bool friction;
    bool callJump;
    bool callContinueJump;
    public PlayerMemory memory;
    public GameObject fadeOut;
    bool dead = false;
    public GameObject mesh;
    public CapsuleCollider m_Collider;
    public GameObject curGround;
    float currentRotation;
    public ChangeGravity changeGrav;
    //Quaternion right;
    //Quaternion left;

    // Start is called before the first frame update
    void Start()
    {
        grounded = false;
        currentSpeed = 0.0f;
        jumping = false;
        friction = false;
        callJump = false;
        currentRotation = model.transform.localEulerAngles.y;
        fadeOut.SetActive(false);
        //memory.SetRespawn(Respawn);
        //right = Quaternion.Euler(model.transform.forward);
        //left = Quaternion.Euler(-1*model.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
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
                currentRotation += rotateSpeed;
                currentRotation = Mathf.Min(90f, currentRotation);
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, currentRotation, model.transform.localEulerAngles.z);
            }
            else
            {
                currentSpeed -= speedUpStep;
                currentSpeed = Mathf.Max(currentSpeed, -1f * maxSpeed);
                direction = 0f;
                currentRotation -= rotateSpeed;
                currentRotation = Mathf.Max(-90f, currentRotation);
                model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, currentRotation, model.transform.localEulerAngles.z);
            }
            Debug.Log(currentRotation);
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
                //Jump();
                callJump = true;
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
                    //ContinueJump();
                    callContinueJump = true;
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
        if (dead)
        {
            return;
        }
        rb.AddForce(-1f * transform.up * fallingSpeed, ForceMode.VelocityChange);
        if (callJump)
        {
            Jump();
            callJump = false;
        }
        else if (callContinueJump)
        {
            ContinueJump();
            callContinueJump = false;
        }
        //if (!grounded || grounded)
        //{
        //    Vector3 newX = rb.velocity;
        //    newX.x = 0f;
        //    rb.velocity = newX;
        //}
    }

    //void Move(Vector3 move)
    //{
    //    rb.AddForce(move*10f, ForceMode.VelocityChange);
    //}

    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode.VelocityChange);
        currentJumpTime = 0f;
        Debug.Log("Here!");
        jumping = true;
    }

    void ContinueJump()
    {
        rb.AddForce(transform.up * jumpStep, ForceMode.VelocityChange);
    }

    public void SetFriction(bool set, Transform myParent)
    {
        if (set)
        {
            gameObject.transform.parent = myParent;
        }
        else
        {
            gameObject.transform.parent = null;
        }
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
        rb.AddForce(-1*transform.up * fallingSpeed, ForceMode.Acceleration);
        jumping = false;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnTimer());
    }

    IEnumerator RespawnTimer()
    {
        dead = true;
        mesh.SetActive(false);
        currentSpeed = 0f;
        m_Collider.enabled = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        transform.parent = null;
        fadeOut.SetActive(true);
        changeGrav.dead = true;
        yield return new WaitForSeconds(1f);
        transform.position = memory.GetRespawnPos();
        transform.rotation = Quaternion.identity;
        mesh.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        m_Collider.enabled = true;
        rb.useGravity = true;
        yield return new WaitForSeconds(0.75f);
        dead = false;
        fadeOut.SetActive(false);
        changeGrav.dead = false;
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
