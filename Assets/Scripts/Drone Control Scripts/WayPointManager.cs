using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    private Rigidbody droneRb;
    private PositionController dronePositionController;
    private DroneDynamics droneDynamics;
    private Rigidbody landingPoint;
    private Rigidbody referenceRb;
    private Rigidbody leaderRb;
    private List<Vector3> trajectory;


    private bool autoTakeOff ;
    private bool autoLanding;
    private bool goToReference;

    public Vector3 target;
    public Vector3 takeOffTarget;


    private DroneManager droneManager;

    // Start is called before the first frame update
    void Start()
    {
        droneRb = GetComponent<Rigidbody>();
        dronePositionController = GetComponent<PositionController>();
        landingPoint = GameObject.Find("Landing Point").GetComponent<Rigidbody>();
        leaderRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        autoLanding = false;
        referenceRb = GameObject.Find("Reference").GetComponent<Rigidbody>();
        takeOffTarget = new Vector3(droneRb.position.x, 10, droneRb.position.z);
        target = new Vector3(droneRb.position.x, droneRb.position.y, droneRb.position.z);
        droneManager = GetComponent<DroneManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        WatchVariables();
        SetTarget();
    }

    void SetTarget(){
        // landing
        if(autoLanding){
            target = landingPoint.position;
        }
        // take off
        else if (autoTakeOff){
            takeOffTarget = new Vector3(droneRb.position.x, 10, droneRb.position.z);
            target = takeOffTarget;
            float distace = (droneRb.position - target).magnitude;
            // / take off is finished then drone proceeds to follow the reference
            if ( distace < 0.2f){ 
                droneManager.autoTakeOff = false;
                droneManager.goToReference = true;
            }
         }
        // follow waypoint
        else if (goToReference){
            // get drone mananer component
            DroneManager droneManager = droneRb.GetComponent<DroneManager>();
            // get reference position
            Vector3 referencePosition = droneManager.referenceTarget;
            target = referencePosition;
            //trajectory =  GenerateTrajectory();
            // if trajectory is calculated then drone will follow the trajectory
            //isTrajectoryCalculated = true;
            //i = 0;

         }
         /*
         else if (goToReference && isTrajectoryCalculated){
            // get drone mananer component
            DroneManager droneManager = droneRb.GetComponent<DroneManager>();
            // get reference position
            Vector3 referencePosition = droneManager.referenceTarget;
            target = referencePosition;
            // go to next point in trajectory
            target = trajectory[i++];
            if (i >= trajectory.Count){
                i = 0;
                isTrajectoryCalculated = false;
            }
         }*/

         // Attack target
         else{
            target = new Vector3(droneRb.position.x, droneRb.position.y, droneRb.position.z);
         }


    }

    void WatchVariables(){
        autoLanding = droneManager.autoLanding;
        autoTakeOff = droneManager.autoTakeOff;
        goToReference = droneManager.goToReference;
    }

    List<Vector3> GenerateTrajectory(){
        Vector3 desiredPosition = target;
        Vector3 desiredVelocity = (desiredPosition - droneRb.position) / Time.fixedDeltaTime;

        // list of points to draw the trajectory
        List<Vector3> trajectory = new List<Vector3>();
        // list of points to draw the trajectory

        // lerp to the target
        for (int i = 0; i < 3; i++){
            Vector3 currentPosition = droneRb.position;
            Vector3 currentVelocity = droneRb.velocity;
            Vector3 acceleration = (desiredVelocity - currentVelocity) / Time.fixedDeltaTime;
            Vector3 nextPosition = currentPosition + currentVelocity * Time.fixedDeltaTime + 0.5f * acceleration * Time.fixedDeltaTime * Time.fixedDeltaTime;
            trajectory.Add(nextPosition);
        }

        return trajectory;
    }

}
