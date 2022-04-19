using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public float maxDistance = 100f;
    private Rigidbody droneRb;
    public Vector3 velocity;
    public Vector3 forceVector;
    public DroneManager _dm;
    private PositionController positionController;
    public string _tag;
    private float maxDistanceSensor = 3f;


    // Start is called before the first frame update
    void Start()
    {
        // get drone rigidbody
        droneRb = GetComponent<Rigidbody>();
        velocity = droneRb.velocity;
        positionController = droneRb.GetComponent<PositionController>();
        _dm = GetComponentInParent<DroneManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = droneRb.velocity;
        forceVector = positionController.forceVector;

        RaycastHit hit;
        Ray landingRay = new Ray(droneRb.transform.position, Vector3.down);
        Vector3 velVector = transform.TransformDirection(velocity) / velocity.magnitude * maxDistance;
        Vector3 forceVec = transform.TransformDirection(forceVector) / forceVector.magnitude * maxDistance;

        Vector3 forceX = transform.TransformDirection(new Vector3(forceVector.x, 0, 0)) / forceVector.magnitude * maxDistance;
        Vector3 forceY = transform.TransformDirection(new Vector3(0, forceVector.y, 0)) / forceVector.magnitude * maxDistance;
        Vector3 forceZ = transform.TransformDirection(new Vector3(0, 0, forceVector.z)) / forceVector.magnitude * maxDistance;

        Debug.DrawRay(transform.position, velVector, Color.green);
        Debug.DrawRay(transform.position, forceVec, Color.blue);
        Debug.DrawRay(transform.position, forceX*maxDistance, Color.red);
        Debug.DrawRay(transform.position, forceY*maxDistance, Color.red);
        Debug.DrawRay(transform.position, forceZ*maxDistance, Color.red);


        if (Physics.Raycast(landingRay, out hit, maxDistance))
        {
            //Debug.Log("Hit: " + hit.transform.name);
        }

        // Check Sensors
        Sensors();


        
    }

    private void Sensors(){

        RaycastHit hit;
        Ray frontSensor = new Ray(droneRb.transform.position, Vector3.forward);
        Ray leftSensor = new Ray(droneRb.transform.position, Vector3.left);
        Ray rightSensor = new Ray(droneRb.transform.position, Vector3.right);
        Ray backSensor = new Ray(droneRb.transform.position, Vector3.back);

        // draw lines
        Debug.DrawRay(transform.position, frontSensor.direction * maxDistanceSensor, Color.green);
        Debug.DrawRay(transform.position, leftSensor.direction * maxDistanceSensor, Color.green);
        Debug.DrawRay(transform.position, rightSensor.direction * maxDistanceSensor, Color.green);
        Debug.DrawRay(transform.position, backSensor.direction * maxDistanceSensor, Color.green);

        

        if (Physics.Raycast(frontSensor, out hit, maxDistance))
        {
            // saves the tag of the object hit
            _tag = hit.transform.tag + " Front";
        
        }


        if (Physics.Raycast(leftSensor, out hit, maxDistance))
        {
            // saves the tag of the object hit
            _tag = hit.transform.tag + " Left";
        }


        if (Physics.Raycast(rightSensor, out hit, maxDistance))
        {
            // saves the tag of the object hit
            _tag = hit.transform.tag + " Right";

        }


        if (Physics.Raycast(backSensor, out hit, maxDistance))
        {
            // saves the tag of the object hit
            _tag = hit.transform.tag + " Back";
        }





    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red, 2, false);

        // Only destroy the drone if it got hit and velocity is high enough
        if (collision.relativeVelocity.magnitude > 3)
        {
            _dm.isDestroyed = true;
            _dm.isDroneOn = false;
        }

    }


}
