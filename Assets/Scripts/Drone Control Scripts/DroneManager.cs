using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    private Rigidbody droneRb;
    private PositionController dronePosition;
    private DroneDynamics droneDynamics;
    private Vector3 initialPos;
    public Rigidbody leaderRb;
    public List<Rigidbody> loyalRbs;

    public bool isDroneOn;
    public bool autoTakeOff;
    public bool autoLanding;
    public bool goToReference;
    public bool goToLeader;
    public bool isDestroyed;
    // point to follow
    public Vector3 referenceTarget;

    // Start is called before the first frame update
    void Start()
    {
        droneRb = GetComponent<Rigidbody>();
        dronePosition = GetComponent<PositionController>();
        initialPos = droneRb.position;
        droneDynamics = GetComponent<DroneDynamics>();
        leaderRb = GameObject.Find("Player").GetComponent<Rigidbody>();
        // get all loyal wingmen
        GameObject[] wingmen = GameObject.FindGameObjectsWithTag("LoyalWingman");
        foreach (GameObject wingman in wingmen)
            loyalRbs.Add(wingman.GetComponent<Rigidbody>());
            

        isDroneOn = false;
        autoTakeOff = false;
        autoLanding = false;
        goToReference = false;
        goToLeader = false;
        isDestroyed = false;

    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
    }

    private void ReadInputs(){       
        // arrow up
        if(Input.GetKeyDown(KeyCode.L)){
            // set auto landing to true/false
            if(isDroneOn){
                autoLanding = !autoLanding;
                goToReference = false;
                autoTakeOff = false;
            }
                
        }
 
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        // Turns DRONE ON/OFF and manual take off 
        if(Input.GetKeyDown(KeyCode.Z))
        {
            isDroneOn = !isDroneOn; 
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            // drone goes to Vector3(x, 1 , z ): 1 meter above the ground
            // once the drone is on, it will go to the target
            isDroneOn = !isDroneOn;
            if(!isDroneOn)
                isDroneOn = !isDroneOn;

            autoTakeOff = !autoTakeOff;
            autoLanding = false;
        }
        if(Input.GetKeyDown(KeyCode.F)) // follow reference
        {
            if(isDroneOn)
            {
                autoTakeOff = false;
                autoLanding = false;
                goToReference = !goToReference;
            }
  
        }
    }


    void Reset(){
        droneRb.transform.position = initialPos;
        droneRb.velocity = Vector3.zero;
        droneRb.angularVelocity = Vector3.zero;
        droneRb.transform.rotation = Quaternion.identity;
        
        autoLanding = false;
        autoTakeOff = false;
        goToReference = false;
        isDroneOn = false;
        isDestroyed = false;
    }

    public void GoTo(Vector3 target){
        referenceTarget = target;
    }

    public bool GetisDestroyed(){
        return isDestroyed;
    }

}
