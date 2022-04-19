using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpawner : MonoBehaviour
{
    private GameObject referenceRb;
    private GameObject droneRb;
    public float distanceTouch;
    // Start is called before the first frame update
    void Start()
    {
        referenceRb = GameObject.Find("Reference");
        droneRb = GameObject.Find("KamikazeBody");
    }

    // Update is called once per frame
    void Update()
    {
        DetectDroneTouch();
    }

    private void DetectDroneTouch(){
        // detect if drone is touching the waypoint
        // if yes, next waypoint
        float distance = Vector3.Distance(droneRb.transform.position, referenceRb.transform.position);
        if(distance < distanceTouch)
            NextWaypoint();
    }
    private void NextWaypoint(){
        // get next random waypoint
        // if no waypoint, end
        // if yes, go to next waypoint
        referenceRb.transform.position = new Vector3(Random.Range(-10,10)  , Random.Range(10,20) , Random.Range(-10,10) );
    }


}
