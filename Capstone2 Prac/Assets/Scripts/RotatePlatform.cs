using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    public float rotateSpeed;
    public bool clockwise = true;
    public CharacterMovement player;
    public bool done = false;
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
        transform.Rotate(0f,0f,rotateSpeed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.SetFriction(true, transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.SetFriction(false, transform);
        }
    }
}
