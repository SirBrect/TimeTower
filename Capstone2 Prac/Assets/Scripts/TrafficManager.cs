using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public List<GameObject> cars;
    public float dist = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject car in cars)
        {
            if(Vector3.Distance(car.transform.position,end.position) <= dist)
            {
                car.transform.position = start.position;
                car.GetComponent<MoveObject>().ChangeTarget();
            }
        }
    }
}
