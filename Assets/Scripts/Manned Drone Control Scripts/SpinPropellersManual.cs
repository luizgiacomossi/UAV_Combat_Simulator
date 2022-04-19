using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellersManual : MonoBehaviour
{
    private float propellerSpeed = 950.0f;
    private Rigidbody droneRb;
    private ManualDroneManager droneManager;
    private float droneVelocity;
    public List<GameObject> propellers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        // get droneBody 
        droneRb = GetComponent<Rigidbody>();
        // get droneManager
        // if tag is player get player manager

        droneManager = droneRb.GetComponent<ManualDroneManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        // spin propellers according to drone velocity 
        droneVelocity = droneRb.velocity.magnitude;        
        // is drone on?
        bool isDroneOn = droneManager.isDroneOn;
        // spin propeller
        if(isDroneOn){

            // iterate through propellers
            foreach(GameObject propeller in propellers){
                // spin propeller
                propeller.transform.Rotate(Vector3.up, droneVelocity * propellerSpeed/10f * Time.deltaTime + propellerSpeed * Time.deltaTime );
            }

        }
    }
}
