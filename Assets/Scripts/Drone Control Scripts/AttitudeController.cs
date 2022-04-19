using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttitudeController : MonoBehaviour
{
    private Rigidbody droneRb;
    private ControlAllocator controlAllocator;
    private Vector3 force;
    private DroneDynamics droneDynamics;
    private Rigidbody referenceRb;
    private GameObject lookReferenceRb;
    private DroneManager droneManager;


    private float psiR; //   Yaw angle refenrece   - y axis kp = 0.85 e kv = 0.7
    private float phiR; //   Roll angle reference  - z axis
    private float thetaR; // Pitch angle reference - x axis
    private float memoryYaw;

    public float kp_pitch;
    public float kv_pitch;

    public float kp_roll;
    public float kv_roll;

    public float kp_yaw;
    public float kv_yaw;

    private bool isDroneOn = false;

    private float k;
    private float m;
    private float d;

    public float tauMax; // roll and pitch
    public float tauMin;
    public Vector3 torque;

    // yaw tau max and min
    private float yawTauMax; // roll and pitch
    private float yawTauMin;

    // Start is called before the first frame update
    void Start()
    {
        droneRb = GetComponent<Rigidbody>();
        controlAllocator = GetComponent<ControlAllocator>();
        droneDynamics = GetComponent<DroneDynamics>();
        droneManager = GetComponent<DroneManager>();
                
        referenceRb = GameObject.Find("Reference").GetComponent<Rigidbody>();

        lookReferenceRb = GameObject.Find("Protected Area");
                // get k 
        k = droneDynamics.k;
        // get m
        m = droneRb.mass;
        // get d
        d = droneDynamics.d;
        //  tau max and min
        tauMax = 4 * m  * d;
        tauMin = - tauMax;

        // for yaw
        yawTauMax = 4 * m * 9.81f * k;
        yawTauMin = - yawTauMax;


    }

    // Update is called once per frame
    void Update()
    {
        isDroneOn = droneManager.isDroneOn;
        k = droneDynamics.k;
        // get m
        m = droneRb.mass;
        // get d
        d = droneDynamics.d;
        //  tau max and min
        tauMax = 4 * m  * d;
        tauMin = - tauMax;

        // for yaw
        yawTauMax = 4 * m * 9.81f * k;
        yawTauMin = - yawTauMax;

        // get drone inertia
        Vector3 droneInertia = droneRb.inertiaTensor;

        // gains
        float wn = 2f;
        float E = 1.4f;
       // kp_pitch = 2 * droneInertia.x * E * wn;
       // kv_pitch = droneInertia.x * wn * wn;


    }

    private void FixedUpdate() {

        Vector3 angularVelocity = droneRb.angularVelocity;

        //   Roll angle reference  - z axis
        float phiCurrent = droneRb.transform.localEulerAngles.z * Mathf.PI / 180;
        phiR = controlAllocator.phiR;
        float torqueZ ;
        if (phiCurrent > 1 ){
            torqueZ = AttitudeControllerPV( phiCurrent , phiR + 2 * Mathf.PI, angularVelocity.z, kp_roll, kv_roll);
        }
        else{
            torqueZ = AttitudeControllerPV( phiCurrent, phiR, angularVelocity.z, kp_roll, kv_roll);
        }

    
        // Pitch angle reference - x axis
        float thetaCurrent = droneRb.transform.localEulerAngles.x * Mathf.PI / 180;
        thetaR = controlAllocator.thetaR;
        float torqueX ;
        if (thetaCurrent > Mathf.PI ){
            torqueX = AttitudeControllerPV( thetaCurrent , thetaR + 2 * Mathf.PI, angularVelocity.x,  kp_pitch, kv_pitch);
        }
        else{
            torqueX = AttitudeControllerPV( thetaCurrent, thetaR, angularVelocity.x, kp_pitch, kv_pitch);
        }

        //   Yaw angle refenrece   - y axis kp = 0.85 e kv = 0.7
        float psiCurrent = droneRb.transform.localEulerAngles.y * Mathf.PI / 180;
        psiR = controlAllocator.psiR;

        Vector3 distance = (referenceRb.transform.position - droneRb.transform.position);
        if (distance.magnitude > 1f)
            psiR = Mathf.Atan2(distance.x, distance.z);
        //psiR = Mathf.Clamp(psiR,  -Mathf.PI/6,  Mathf.PI/6);
        //alway look ahead
        psiR = 0f;

        float torqueY;
        if (psiCurrent > Mathf.PI ){
            torqueY = AttitudeControllerPV( psiCurrent , psiR + 2 * Mathf.PI, angularVelocity.y,  kp_yaw, kv_yaw);
        }
        else{
            torqueY = AttitudeControllerPV( psiCurrent, psiR, angularVelocity.y, kp_yaw, kv_yaw);
        }

        // Apply desired torque to drone
        torque = new Vector3(torqueX, torqueY, torqueZ);
        ApplyTorqueToDrone(torque);

    }


    private float AttitudeControllerPV(float currentValue, float desiredValue, float angVelocity, float kp = 0.85f, float kv = 0.7f) {
        float error = desiredValue - currentValue;
        float torque = kp * error - kv * angVelocity;
        return torque;
    }

    private Vector3 LimitTorque(Vector3 torqueLimit) {
        if(torqueLimit.x > tauMax) {
            torqueLimit.x = tauMax;
        } else if(torqueLimit.x < tauMin) {
            torqueLimit.x = tauMin;
        }
        if(torqueLimit.y > yawTauMax) {
            torqueLimit.y = yawTauMax;
        } else if(torqueLimit.y < yawTauMin) {
            torqueLimit.y = yawTauMin;
        }
        if(torqueLimit.z > tauMax) { 
            torqueLimit.z = tauMax;
        } else if(torqueLimit.z < tauMin) {
            torqueLimit.z = tauMin;
        }
        return torqueLimit;
    }

    private void GetComponentsNeeded(){

    }

    void ApplyTorqueToDrone(Vector3 torqueApply){
        if(isDroneOn){
            // limit torque to the max torque of the rotor
            torqueApply = LimitTorque(torqueApply);
            droneRb.AddRelativeTorque(torqueApply, ForceMode.Force);
        }
    }



}
