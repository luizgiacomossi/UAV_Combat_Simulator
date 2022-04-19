using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDynamics : MonoBehaviour
{
    private Rigidbody goalRb; 
    private Rigidbody referenceRb;

    public float kp_att ; 
    public float kv_att ; 
    public Vector3 desiredRotation;

   // Drones Dynamic Parameters
    private float uavMass ;

    // calculates drone dynamics
    private float l = 0.0624f;
    public float d ;

    // constants
    private float Ct = 0.0107f;
    private float Cq = 0.00078264f;
    private float r = 0.0330f;
    private float A = 0.00342f;
    private float rho = 1.184f;
    private float kf ;
    private float ktau ;

    private float fRotorMin = 0.0f ;
    public float fRotorMax;
    private float wMin;
    private float wMax;
    public float k;
    private Vector3 initialPos;
    private Vector3 forceToApply ;
    private float phir;
    private float thetar;
    public float psir;
    public bool isDroneOn = false;
    // Start is called before the first frame update
    void Start()
    {
        d = l * Mathf.Sqrt(2.0f) / 2.0f;
        kf = Ct *  Mathf.Pow(r, 2.0f) * A * rho;
        ktau = Cq * Mathf.Pow(r, 3.0f) * A * rho;

        uavMass = GetComponent<Rigidbody>().mass;

        fRotorMax = 2.0f * uavMass * -1 * Physics.gravity.y;
        
        wMin = Mathf.Sqrt(fRotorMin / kf);
        wMax = Mathf.Sqrt(fRotorMax / kf);

        k = ktau / kf; 

        goalRb = GetComponent<Rigidbody>();
        referenceRb = GameObject.Find("Reference").GetComponent<Rigidbody>();
        
        initialPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = goalRb.velocity;
    }


    private float limitForce(float force ,float infLimit, float supLimit){
        if(force > supLimit){
            return supLimit;
        }
        else if(force < infLimit){
            return infLimit;
        }
        else{
            return force;
        }
    }


}
