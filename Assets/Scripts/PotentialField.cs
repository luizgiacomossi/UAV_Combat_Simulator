using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialField : MonoBehaviour
{
    private Rigidbody droneRb;  
    private PositionController dronePositionController;
    private DroneManager droneManager;
    private DroneDynamics droneDynamics;
    private ControlAllocator controlAllocator;

    public Vector3 force;
    public Vector3 derivative;
    public float forceModifier;
    public float fRotorMax;
    // collider
    public Collider droneCollider;

    public bool collided;

    // Start is called before the first frame update
    void Start()
    {
        // up in the hierarchy get drone manager
        droneManager = GetComponentInParent<DroneManager>();
        droneDynamics = GetComponentInParent<DroneDynamics>();
        
        droneRb = GetComponentInParent<Rigidbody>();
        dronePositionController = GetComponentInParent<PositionController>();
        controlAllocator = GetComponentInParent<ControlAllocator>();

        droneCollider = GetComponent<Collider>();

        collided = false;
        fRotorMax = droneDynamics.fRotorMax;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fRotorMax = droneDynamics.fRotorMax;

    }

    float NormalFunction(float omega,Vector3 center, Vector3 position){
        float f =  Mathf.Exp( -omega*((position.x - center.x) + (position.y - center.y)));
        return f;
    }
      
    float BivariateFunction(float alpha,float beta, Vector3 center,Vector3 position){
        float k = 10f;
        float f =  Mathf.Exp( -alpha*(position.x - center.x)/k*k - beta*(position.y - center.y)/k*k  - alpha*(position.z - center.z)/k*k);
        return f;
    }

    
    Vector3 DerivativeBivariate(float alpha,float beta, float omega, Vector3 center,Vector3 position){
        float f = BivariateFunction(alpha,beta,center,position);
        float dx = f * (-2*alpha*(position.x-center.x));
        float dy = f * (-2*beta*(position.y-center.y));
        float dz = f * (-2*omega*(position.z-center.z));
        return new Vector3(dx,dy,dz);
    }
 
    // on trigger

    // on trigger pressure
    void OnTriggerStay(Collider other){
        bool isDroneOn = droneManager.isDroneOn;

        if (isDroneOn){
            if(other.gameObject.tag != "Ground"){
            // calculate force based on bivariate function
            derivative = DerivativeBivariate(forceModifier,forceModifier ,forceModifier, other.transform.position,  droneRb.position);
            // velocity
            force = -derivative ; //+  derivative;
            force = limitForce(force, 0, fRotorMax);

            // update force in controll allocator
            controlAllocator.UpdateForceField(force);
            
            //droneRb.AddRelativeForce(force);
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // if drone is on

        Debug.Log(droneRb.gameObject.name+ " collided with : " + other.gameObject.name);
        collided = true;

    }

    // on trigger exit
    void OnTriggerExit(Collider other)
    {
        collided = false;
        controlAllocator.UpdateForceField(new Vector3(0,0,0));
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

}


