using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualDroneManager : MonoBehaviour
{
    private Rigidbody droneRb;
    private DroneDynamics droneDynamics;
    private Vector3 initialPos;
    public ParticleSystem explosion;

    public bool isDroneOn;
    public bool autoTakeOff;
    public bool autoLanding;
    public bool goToReference;
    public bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        
        droneRb = GetComponent<Rigidbody>();
        initialPos = droneRb.position;
        droneDynamics = GetComponent<DroneDynamics>();

        isDroneOn = false;
        autoTakeOff = false;
        autoLanding = false;
        isDestroyed = false;

    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();

        if(isDestroyed){
            explosion.Play();
        }
        else{
            explosion.Stop();
        }
    }


    private void ReadInputs(){

            if(Input.GetKeyDown(KeyCode.Space)){
                // set auto landing to true/false
                if(isDroneOn){
                    autoTakeOff = !autoTakeOff;
                }
                    
            }
            // reset input
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }

            // Turns DRONE ON/OFF and manual take off 
            if(Input.GetKeyDown(KeyCode.Z))
            {
                isDroneOn = !isDroneOn; 
            }


    }


    void Reset(){
        droneRb.transform.position = initialPos;
        droneRb.velocity = Vector3.zero;
        droneRb.angularVelocity = Vector3.zero;
        droneRb.transform.rotation = Quaternion.identity;

        isDestroyed = false;
        isDroneOn = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red, 2, false);

        // Only destroy the drone if it got hit and velocity is high enough
        if (collision.relativeVelocity.magnitude > 1)
        {
            isDestroyed = true;
            isDroneOn = false;
        }

    }




}
