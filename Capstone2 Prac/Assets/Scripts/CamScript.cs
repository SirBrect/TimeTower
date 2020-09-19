using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform player;
    public CharacterMovement cm;
    Vector3 followPos;
    public float smooth;
    public float fallSmooth;
    public float yOffset;
    float yPos;
    float yDist;
    // Start is called before the first frame update
    void Start()
    {
        yPos = player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        followPos = new Vector3(player.position.x, yPos + yOffset, transform.position.z);
        //followPos = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, followPos, smooth);
        yDist = player.position.y - yPos;
        if ((yDist >= 5.5f && cm.IsGrounded())|| yDist < 0f)
        {
            MoveCamY();
        }
    }

    public void MoveCamY()
    {
        yPos = player.position.y;
    }
}
