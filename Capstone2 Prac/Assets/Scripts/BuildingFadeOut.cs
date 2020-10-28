using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFadeOut : MonoBehaviour
{
    public MeshRenderer mr;
    public List<BoxCollider> on;
    public List<BoxCollider> off;
    bool buildingOn;
    // Start is called before the first frame update
    void Start()
    {
        buildingOn = true;
        foreach (BoxCollider box in off)
        {
            box.enabled = false;
        }
        foreach (BoxCollider box in on)
        {
            box.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SwitchView());
    }

    IEnumerator SwitchView()
    {
        yield return new WaitForSeconds(0.15f);
        if (buildingOn)
        {
            buildingOn = false;
            mr.enabled = false;
            foreach (BoxCollider box in on)
            {
                box.enabled = false;
            }
            foreach (BoxCollider box in off)
            {
                box.enabled = true;
            }
        }
        else
        {
            buildingOn = true;
            mr.enabled = true;
            foreach (BoxCollider box in off)
            {
                box.enabled = false;
            }
            foreach (BoxCollider box in on)
            {
                box.enabled = true;
            }
        }
    }
}
