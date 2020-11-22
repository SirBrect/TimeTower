using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    public float rotateSpeed;
    public bool clockwise = true;
    public CharacterMovement player;
    public bool done = false;
    public int axis = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            return;
        }
        if(player.isDead() && Time.timeScale < 0.1f)
        {
            return;
        }
        // X rotation
        if (axis == 0)
        {
            transform.Rotate(rotateSpeed, 0f, 0f);
        }
        // Y rotation
        else if(axis == 1)
        {
            transform.Rotate(0f, rotateSpeed, 0f);
        }
        // Z rotation
        else
        {
            transform.Rotate(0f, 0f, rotateSpeed);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
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
        player.SetFriction(true, transform);
    }

    public void PlayerOff()
    {
        player.SetFriction(false, transform);
    }
}