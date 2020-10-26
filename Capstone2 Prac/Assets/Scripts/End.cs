using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public GameObject endscreen;
    public Text text;
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        endscreen.SetActive(true);
        text.text = "Nice Work!\nTime: " + timer.GetTime().ToString();
        Time.timeScale = 0.0f;
    }
}
