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
    private float maxSpeed;
    public float maxGroundSpeed;
    public float maxAirSpeed;
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
    public float forceMult;
    [SerializeField]
    private bool debugInvincible = false;
    bool stopMidair = false;

    public Vector3 movementDir;

    public bool stop = false;

    float timer = 0.0f;

    float startTime = 0.0f;

    bool staminaUsed = false;

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
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        maxSpeed = maxGroundSpeed;
        //memory.SetRespawn(Respawn);
        //right = Quaternion.Euler(model.transform.forward);
        //left = Quaternion.Euler(-1*model.transform.forward);
        //FindDirections();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
        if (grounded)
        {
            maxSpeed = maxGroundSpeed;
        }
        else
        {
            maxSpeed = maxAirSpeed;
        }
        float horizontalStick = Input.GetAxisRaw("Horizontal");
        float horizontalPad = Input.GetAxisRaw("HorizontalAlt");
        if(Mathf.Abs(horizontalStick) >= Mathf.Abs(horizontalPad))
        {
            horizontal = horizontalStick;
        }
        else
        {
            horizontal = horizontalPad;
        }
        
        //if (horizontal < .01 && horizontal > -.01 && stop){
            
        //}
        //else{
        //    if(Vector3.Cross(rb.velocity, transform.up).magnitude < maxSpeed)
        //    {
        //        rb.AddForce(transform.right * horizontal * speed, ForceMode.Force);
        //        stop = true;
        //    }
        //}
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (horizontal > 0.0f)
        {
            animator.SetFloat("Direction", 1.0f);
        }
        else if(horizontal< 0.0f)
        {
            animator.SetFloat("Direction", -1.0f);
        }


        // Jump checks
        if ((Input.GetButtonDown("Jump") || Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            if (grounded)
            {
                //Jump();
                callJump = true;
                Debug.Log("Pressed jump!");
            }
        }
        else if (Input.GetButton("Jump") || Input.GetButton("Fire1"))
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

        if (Input.GetButtonDown("ButtonB"))
        {
            if (!debugInvincible)
            {
                debugInvincible = true;
                rb.velocity = Vector3.zero;
                //rb.useGravity = false;
                //rb.detectCollisions = false;
            }
            else
            {
                debugInvincible = false;
                //rb.useGravity = true;
                //rb.detectCollisions = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            return;
        }
        if (!debugInvincible)
        {
            if (Vector3.Cross(rb.velocity, transform.right).magnitude < maxSpeed || jumping)
            {
                rb.AddForce(-1f * transform.up * fallingSpeed, ForceMode.VelocityChange);
            }
        }
        if (grounded && timer > 0.0f){
            Debug.Log(timer);
            timer -= .25f;
            if (timer < 0.0f){
                timer = 0.0f;
            }
            staminaUsed = false;
        }

        debugInvincible = false;
        if (!grounded && Input.GetButton("SquareX") && !staminaUsed){
            rb.velocity = Vector3.zero;
            debugInvincible = true;
            Debug.Log(timer);
            if (stop == true){
                stop = false;
                timer += .25f;
                startTime = Time.deltaTime - timer;
                //timer -= 10;
                //startTime = Time.deltaTime;
            }
            else{
                rb.velocity = Vector3.zero;
                timer += .1f;
                if (Time.deltaTime - startTime >= 3.0f || timer >= 3.0f){
                    staminaUsed = true;
                }
            }
        }

        /*if (horizontal < .01 && horizontal > -.01)
        {
            /*
            if (stop)
            {
                Vector3 curXVelocity = Vector3.Cross(rb.velocity, transform.up);
                rb.velocity = Vector3.zero;
                stop = false;
            }
            
        }*/
        else if(!debugInvincible)
        {
            if (Vector3.Cross(rb.velocity, transform.up).magnitude < maxSpeed)
            {
                rb.AddForce(transform.right * horizontal * speed, ForceMode.Force);
                stop = true;
            }
        }
        if (Vector3.Angle(transform.up, rb.velocity) > 95.0f && rb.mass == 1f)
        {
            Fall();
        }
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
    }

    //void Move(Vector3 move)
    //{
    //    rb.AddForce(move*10f, ForceMode.VelocityChange);
    //}

    void Jump()
    {
        rb.AddForce(transform.up * jumpPower, ForceMode.Force);
        currentJumpTime = 0f;
        Debug.Log("Here!");
        jumping = true;
        stop = true;
    }

    void ContinueJump()
    {
        rb.AddForce(transform.up * jumpStep, ForceMode.Force);
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
        if (!debugInvincible)
        {
            if (Vector3.Cross(rb.velocity, transform.right).magnitude < maxSpeed)
            {
                rb.AddForce(-1 * transform.up * fallingSpeed, ForceMode.Acceleration);
            }
        }
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

    public void Invis()
    {
        dead = true;
        mesh.SetActive(false);
        m_Collider.enabled = false;
        //rb.useGravity = false;
        currentSpeed = 0f;
        rb.velocity = Vector3.zero;
    }

    public void UnInvis()
    {
        dead = false;
        mesh.SetActive(true);
        m_Collider.enabled = true;
        //rb.useGravity = true;
    }

    IEnumerator RespawnTimer()
    {
        Invis();
        transform.parent = null;
        fadeOut.SetActive(true);
        changeGrav.dead = true;
        yield return new WaitForSeconds(1f);
        Vector3 respawnPos = memory.GetRespawnPos();
        transform.position = new Vector3(respawnPos.x, respawnPos.y, transform.position.z);
        transform.rotation = Quaternion.identity;
        mesh.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        m_Collider.enabled = true;
        //rb.useGravity = true;
        yield return new WaitForSeconds(0.75f);
        dead = false;
        fadeOut.SetActive(false);
        changeGrav.dead = false;
        transform.parent = null;
    }
    void FindDirections(){
        Vector3 currentGrav = Physics.gravity;
        movementDir = new Vector3(-currentGrav.y, currentGrav.x, 0.0f);
        movementDir = movementDir.normalized;
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
