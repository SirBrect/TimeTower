using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    private bool ready;
    [SerializeField]
    private bool set;

    bool begin;
    Vector3 origin;
    //BoxCollider beginBox;
    public MeshRenderer mr;
    GameObject player;
    public CharacterMovement character;
    public float speed;
    Vector3 movement;
    Quaternion originRot;
    float gravityAxisVal;
    bool pause;

    // Start is called before the first frame update
    void Start()
    {
        ready = false;
        set = false;
        begin = false;
        origin = transform.position;
        originRot = transform.rotation;
        //beginBox = GetComponent<BoxCollider>();
        player = character.gameObject;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready && (Input.GetButtonDown("SquareX") || Input.GetButtonDown("Interact")) && !begin && !set)
        {
            Set();
            return;
        }
        else if (set && (Input.GetButtonDown("SquareX") || Input.GetButtonDown("Interact")) && !begin && !ready)
        {
            Begin();
            return;
        }
        if (begin)
        {
            if (Input.GetButtonDown("SquareX") || Input.GetButtonDown("Interact"))
            {
                if (pause)
                {
                    pause = false;
                }
                else
                {
                    pause = true;
                }
            }
            if (!pause)
            {
                movement = new Vector3(speed * Time.deltaTime, 0f, 0f);
                transform.Translate(movement);
            }
            float gravityTrig = Input.GetAxis("Gravity");
            float gravityBump = Input.GetAxis("GravitySlow");
            if (Mathf.Abs(gravityTrig) >= Mathf.Abs(gravityBump))
            {
                gravityAxisVal = gravityTrig;
            }
            else
            {
                gravityAxisVal = gravityBump;
            }
            /*
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, 1.0f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, -1.0f);
            }
            */
            transform.Rotate(0, 0, gravityAxisVal);
            player.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {
            ready = true;
        }
        else if(other.tag == "DestroyCar")
        {
            StartCoroutine(DestroyCar());
        }
        else if(other.tag == "FinishCar")
        {
            FinishCar();
            other.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Player")
        {
            ready = false;
        }
    }

    void Begin()
    {
        //beginBox.enabled = false;
        ready = false;
        set = false;
        begin = true;
        pause = false;
        //player.transform.parent = transform;
    }

    void Set()
    {
        ready = false;
        begin = false;
        set = true;
        character.Invis();
    }

    IEnumerator DestroyCar()
    {
        begin = false;
        //mr.enabled = false;
        character.UnInvis();
        transform.position = origin;
        transform.rotation = originRot;
        pause = false;
        //character.Respawn();
        yield return new WaitForSeconds(0.5f);
        //mr.enabled = true;
        //beginBox.enabled = true;
    }

    void FinishCar()
    {
        character.UnInvis();
        transform.position = origin;
        transform.rotation = originRot;
        //beginBox.enabled = true;
        begin = false;
        gameObject.SetActive(false);
    }
}
