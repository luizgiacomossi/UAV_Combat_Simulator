using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    // controller Gains
    public float kp;
    public float kv;
    private float uavMass ;
    private float uavWeight;
    private Vector3 initialPosition;
    private Rigidbody droneRb;
    private Rigidbody referenceRb;
    public DroneDynamics droneDynamics;
    private float fRotorMin = 0.0f ;
    public float fRotorMax;
    public Vector3 forceVector; 
    private bool isDroneOn = false;
    private Vector3 target;
    private Rigidbody landingPoint; 
    private DroneManager droneManager;
    private WayPointManager wayPointManager;

    // Start is called before the first frame update
    void Start()
    {
        // saves initial position to reset the drone
        initialPosition = transform.position;
        GetComponentsNeeded();
        CalculateParameters();
        SetTarget();
    }

    // Update is called once per frame
    void Update(){      
        isDroneOn = droneManager.isDroneOn;
        // set which target the drone will follow
        SetTarget();
    }

    // 
    void FixedUpdate()
    {
        Vector3 desiredPosition = target;
        Vector3 currentPosition = transform.position;
        Vector3 velocity = droneRb.velocity;

        Vector3 force;
        bool isDestroyed = droneManager.isDestroyed;

        if(!isDestroyed){
                    // only apply force if drone is on
            if(isDroneOn){
                force = PositionControllerPV(desiredPosition, currentPosition, velocity);

                int direction = 1;
                if(force.y<0){
                    direction = -1;
                }
                //droneRb.AddRelativeForce(force);
                //droneRb.AddForce(force);
                //droneRb.AddRelativeForce(Vector3.up * force.magnitude * direction); // for more realistic drone

                if(force.y<0){
                    force.y =0;
                }

                if(force.x<0){
                    force.x =0;
                }

                if(force.z<0){
                    force.z =0;
                }
                
                droneRb.AddRelativeForce(Vector3.up * force.magnitude * direction); // for more realistic drone

            }
        }


            
    }

    // kp = 2.21 e kv = 0.41 para o drone menos realista
    // kp = 0.241 e kv = 0.11 para o drone mais realista
    // Position controller using P+V
    Vector3 PositionControllerPV(Vector3 desiredPosition, Vector3 currentPosition, Vector3 velocity){
        Vector3 desiredDirection = ( desiredPosition - currentPosition );
        // Calculates the error between the desired position and the current position in each axis
        Vector3 force = desiredDirection * kp - kv * velocity;
        force.y +=  uavWeight; // adds the weight of the drone as a non-linearity in controll law
        // Apply the force to the drone
        force = limitForce(force, fRotorMin, fRotorMax  ); // * 4 because it have 4 rotors
        forceVector = force; // save force

        // Apply the force to the drone in the normal direction of the rotors
        int direction = 1;
        if( force.y < 0 ) // if direction is negative apply the force to the drone in the opposite direction
            direction = 0;

        return force;

    }

    private Vector3 limitForce(Vector3 force ,float infLimit, float supLimit){
        if(force.magnitude > supLimit){
            return force.normalized * supLimit;
        }
        else if(force.magnitude  < infLimit){
            return force.normalized * infLimit;
        }
        else{
            return force;
        }
    }

    private void GetComponentsNeeded(){
        droneRb = GetComponent<Rigidbody>();
        uavMass = GetComponent<Rigidbody>().mass;
        referenceRb = GameObject.Find("Reference").GetComponent<Rigidbody>();
        landingPoint = GameObject.Find("Landing Point").GetComponent<Rigidbody>();
        droneManager = GetComponent<DroneManager>();
        wayPointManager = GetComponent<WayPointManager>();
    }

    private void CalculateParameters(){
        uavWeight =  uavMass * -1 * Physics.gravity.y; 
        fRotorMax = 2.0f * uavWeight;
    }


    private void SetTarget(){
        // defines a point to follow
        target = wayPointManager.target;
        
    }

}
