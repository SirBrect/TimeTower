using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFadeOut : MonoBehaviour
{
    public List<MeshRenderer> mr;
    public List<BoxCollider> on;
    public List<BoxCollider> off;
    public List<GameObject> portals;
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
        foreach (MeshRenderer mesh in mr)
        {
            mesh.enabled = true;
        }
        foreach(GameObject portal in portals)
        {
            portal.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(buildingOn && mr[0].material.color.a < 1.0f)
        //{
        //    foreach(MeshRenderer mesh in mr)
        //    {
        //        Color newColor = mesh.material.color;
        //        newColor.a -= Time.deltaTime;
        //        mesh.material.color = newColor;
        //    }
        //}
        //else if(!buildingOn && mr[0].material.color.a > 0.0f)
        //{
        //    foreach (MeshRenderer mesh in mr)
        //    {
        //        Color newColor = mesh.material.color;
        //        newColor.a += Time.deltaTime;
        //        mesh.material.color = newColor;
        //    }
        //}
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
            foreach (MeshRenderer mesh in mr)
            {
                mesh.enabled = false;
            }
            //mr.enabled = false;
            foreach (BoxCollider box in on)
            {
                box.enabled = false;
            }
            foreach (BoxCollider box in off)
            {
                box.enabled = true;
            }
            foreach (GameObject portal in portals)
            {
                portal.SetActive(false);
            }
        }
        else
        {
            buildingOn = true;
            foreach (MeshRenderer mesh in mr)
            {
                mesh.enabled = true;
            }
            //mr.enabled = true;
            foreach (BoxCollider box in off)
            {
                box.enabled = false;
            }
            foreach (BoxCollider box in on)
            {
                box.enabled = true;
            }
            foreach (GameObject portal in portals)
            {
                portal.SetActive(true);
            }
        }
    }
}
