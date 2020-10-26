using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThrough : MonoBehaviour
{
    bool isTouching = false;
    BoxCollider box;
    
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching && Input.GetAxisRaw("Vertical")<0.0f && (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Interact")))
        {
            StartCoroutine(Fall());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isTouching = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTouching = false;
    }

    IEnumerator Fall()
    {
        box.enabled = false;
        yield return new WaitForSeconds(0.5f);
        box.enabled = true;
    }
}
