using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPropellers : MonoBehaviour
{
    private float propellerSpeed = 950.0f;
    public Rigidbody droneRb;
    public DroneManager droneManager;
    private float droneVelocity;
    public List<GameObject> propellers ;
    // Start is called before the first frame update
    void Start()
    {
        // get droneBody 
        droneRb = GetComponentInParent<Rigidbody>();
        // get droneManager
        // if tag is player get player manager

        droneManager = droneRb.GetComponent<DroneManager>();

    }

    // Update is called once per 1/20 of a second
    void FixedUpdate()
    {   
        // spin propellers according to drone velocity 
        droneVelocity = droneRb.velocity.magnitude;        
        // is drone on?
        bool isDroneOn = droneManager.isDroneOn;
        bool isDestroyed = droneManager.GetisDestroyed();
        // spin propeller
        if(isDroneOn && !isDestroyed){
            // iterate through propellers
            foreach(GameObject propeller in propellers){
                // spin propeller
                propeller.transform.Rotate(Vector3.up, droneVelocity * propellerSpeed/10f * Time.deltaTime + propellerSpeed * Time.deltaTime );
            }

        }
    }
    
}
