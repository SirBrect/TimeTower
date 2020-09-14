using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground" && !player.IsGrounded())
        {
            player.Ground();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            player.Unground();
        }
    }
}
