using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertGravityChange : MonoBehaviour
{
    public ChangeGravity gravity;
    public GameObject checkmark;
    // Start is called before the first frame update
    void Start()
    {
        checkmark.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvertGravity()
    {
        gravity.Invert();
        if (checkmark.activeInHierarchy)
        {
            checkmark.SetActive(false);
        }
        else
        {
            checkmark.SetActive(true);
        }
    }
}
