using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualController : MonoBehaviour
{
    private Rigidbody droneRb;
    private Vector3 initialPosition;
    private ManualDroneManager droneManager;
    private float uavMass;
    private float uavWeight;
    private float fRotorMin = 0.0f;
    private float fRotorMax;
    private Vector3 forceVector;
    public float desiredHeightFlight = 1.0f;
    public float kp;
    public float kv;

    public float rotX = 0.0f;
    public float rotY = 0.0f;
    public float rotZ = 0.0f;

    private float maxAngle = 30.0f;

    public float angleStep = 0.1f;
    public float heightStep = 0.1f;
    private bool isDroneOn = false;


    public Vector3 desiredAngles = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        droneRb = GetComponent<Rigidbody>();
        droneManager = GetComponent<ManualDroneManager>();
        initialPosition = transform.position;
        GetComponentsNeeded();
        CalculateParameters();

    }

    // Update is called once per frame
    void Update(){    
        isDroneOn = droneManager.isDroneOn;
    }
    
    void FixedUpdate(){
        float currentHeight = transform.position.y;
        Vector3 currentVelocity = droneRb.velocity;


        if(isDroneOn){

            // drone takes off
            float forceY = HeightControllerPV(desiredHeightFlight, currentHeight, currentVelocity);
            droneRb.AddRelativeForce(new Vector3(0, forceY, 0));

            // if up is pressed, add force up
            if(Input.GetKey(KeyCode.UpArrow)){
                desiredHeightFlight += heightStep;
            }
            if(Input.GetKey(KeyCode.DownArrow)){
                desiredHeightFlight -= heightStep;
            }

            if(Input.GetKey(KeyCode.S)){
                if(rotX>-maxAngle)
                    rotX -= angleStep;
            }
            else if(Input.GetKey(KeyCode.W)){
                if(rotX<maxAngle)
                    rotX += angleStep;
            }
            else{
                rotX = 0.0f;
            }

            if(Input.GetKey(KeyCode.A)){
                if(rotZ<maxAngle)
                    rotZ += angleStep;
            }
            else if(Input.GetKey(KeyCode.D)){
                if(rotZ>-maxAngle)
                    rotZ -= angleStep;
            }else{
                rotZ = 0.0f;
            }

            if(Input.GetKey(KeyCode.R)){
                Reset();
            }

            // key unpresed resets the desired angles
            if(!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
                rotX = 0.0f;  
                rotY = 0.0f;
                rotZ = 0.0f;
            }

            desiredAngles = new Vector3(rotX, rotY, rotZ);
        }
 
    }

    void GetComponentsNeeded(){
        droneRb = GetComponent<Rigidbody>();
        uavMass = GetComponent<Rigidbody>().mass;
    }

    private void CalculateParameters(){
        uavWeight =  uavMass * -1 * Physics.gravity.y; 
        fRotorMax = 2.0f * uavWeight;
    }

    private float HeightControllerPV(float desiredHeight, float currentHeight, Vector3 velocity){
        float forceY = 0.0f;
        float error = desiredHeight - currentHeight;
        forceY = kp * error - kv * velocity.y + uavWeight;

        forceY = Mathf.Clamp(forceY, fRotorMin, fRotorMax);
        
        return forceY;
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

    private void Reset() {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        droneRb.velocity = Vector3.zero;
        droneRb.angularVelocity = Vector3.zero;
    }
}
