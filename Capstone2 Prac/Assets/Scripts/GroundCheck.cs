using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterMovement player;
    int layerMask;
    RaycastHit hit;
    public bool usingRay = true;
    public float distance = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 12;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!usingRay)
        {
            return;
        }
        Debug.DrawRay(transform.position, -1*transform.parent.up, Color.red, distance);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),out hit, distance, layerMask))
        {
            if (!player.IsGrounded())
            {
                player.Ground();
            }
            //player.Ground();
        }
        else
        {
            if (player.IsGrounded())
            {
                player.Unground();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!usingRay && (other.gameObject.tag == "Ground" || other.gameObject.tag == "FinishCar") && !player.IsGrounded())
        {
            player.Ground();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (!usingRay && other.gameObject.tag == "Ground")
        {
            player.Unground();
        }
    }
}
