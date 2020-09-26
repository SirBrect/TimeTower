using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFirst = false;
    public PlayerMemory mem;
    // Start is called before the first frame update
    void Start()
    {
        if (isFirst)
        {
            mem.SetRespawn(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
