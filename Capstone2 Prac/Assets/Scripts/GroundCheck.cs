using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterMovement player;
    int layerMask;
    RaycastHit hit;
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
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down),out hit, 0.5f, layerMask))
        {
            if (!player.IsGrounded())
            {
                player.Ground();
            }
        }
        else
        {
            if (player.IsGrounded())
            {
                player.Unground();
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Ground" && !player.IsGrounded())
    //    {
    //        player.Ground();
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Ground")
    //    {
    //        player.Unground();
    //    }
    //}
}
