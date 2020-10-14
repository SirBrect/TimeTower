using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatformChild : MonoBehaviour
{
    public RotatePlatform rotate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rotate.PlayerOn();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rotate.PlayerOff();
        }
    }
}
