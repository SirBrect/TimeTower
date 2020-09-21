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
    public float lookDown;
    float yPos;
    float yDist;
    float currentYOffset;
    // Start is called before the first frame update
    void Start()
    {
        yPos = player.position.y;
        currentYOffset = yOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //followPos = new Vector3(player.position.x, yPos + yOffset, transform.position.z);
        //followPos = new Vector3(player.position.x, player.position.y + yOffset, transform.position.z);
        //followPos = new Vector3(player.position.x, player.position.y + currentYOffset, transform.position.z);
        followPos = new Vector3(player.position.x, yPos + currentYOffset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, followPos, smooth);
        yDist = player.position.y - yPos;
        if ((yDist >= 5f && cm.IsGrounded())|| yDist < 0f)
        {
            MoveCamY();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(WaitDown());
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            StopAllCoroutines();
            currentYOffset = yOffset;
        }
    }

    public void MoveCamY()
    {
        yPos = player.position.y;
    }

    IEnumerator WaitDown()
    {
        yield return new WaitForSeconds(0.25f);
        currentYOffset = lookDown;
    }
}
