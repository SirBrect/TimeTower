using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public List<Transform> trans;
    public float speed;
    private List<Vector3> pos;
    int currentIndex;
    Vector3 targetPos;
    float startTime;
    float distanceMeasure = 1f;
    public bool loop = true;
    bool done = false;
    Rigidbody rb;
    public CharacterMovement player;
    public bool slowdown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        pos = new List<Vector3>();
        foreach (Transform form in trans)
        {
            pos.Add(form.position);
        }
        currentIndex = 0;
        targetPos = pos[0];
        startTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            return;
        }

        //transform.Translate((targetPos - transform.position).normalized * speed * Time.deltaTime);
        float curSpeed = speed;
        float distance = Vector3.Distance(transform.position, targetPos);
        if (slowdown && distance <= 2.0f)
        {
            curSpeed = speed * Mathf.Max((distance - 1.0f), 0.2f);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, curSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position,targetPos) <= distanceMeasure)
        {
            //Debug.Log("Made it!");
            ChangeTarget();
        }
    }

    void ChangeTarget()
    {
        currentIndex++;
        if (currentIndex >= pos.Count)
        {
            if (loop)
            {
                currentIndex = 0;
                //Debug.Log("Changing targetPos from: " + targetPos);
            }
            else
            {
                done = true;
                return;
            }
        }
        targetPos = pos[currentIndex];
        startTime = Time.deltaTime;
        //Debug.Log("to: " + targetPos);
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerOn();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerOff();
        }
    }

    public void PlayerOn()
    {
        if (player.IsGrounded())
        {
            player.SetFriction(true, transform);
        }
    }

    public void PlayerOff()
    {
        player.SetFriction(false, transform);
    }
}
