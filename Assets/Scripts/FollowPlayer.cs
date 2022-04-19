using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public Vector3 offset = new Vector3(0, 1.74f, -2.9f);
    private int focus ; 
    public float cameraVibrationSpeed ;
    // list of all drones in scene
    private GameObject[] drones;    
    private GameObject swarmCenter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        // gets all loyal wingman drones in scene
        drones = GameObject.FindGameObjectsWithTag("LoyalWingman");

        // gets the swarm center
        swarmCenter = GameObject.FindGameObjectWithTag("LoyalSwarm");

        focus = 10;

    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();
        SetFocusCamera();

    }

    void ReadInputs(){
        // reads input from keyboard 
        if (Input.GetKeyDown(KeyCode.W))
        {
            // camera focus on player
            focus = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // camera focus on first loyal wingman
            focus = 1; 
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // camera focus on scnd loyal wingman
            focus = 2; 
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // camera focus on third loyal wingman
            focus = 3; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // camera focus on fourth loyal wingman
            focus = 4; 
        }   
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // camera focus on third loyal wingman
            focus = 5; 
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // camera focus on third loyal wingman
            focus = 10; 
        }
    }

    void SetFocusCamera(){
        if(focus == 0){
        // Camera follow player position
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + offset;
            transform.rotation = Quaternion.Euler(30.112f + player.transform.rotation.x * cameraVibrationSpeed ,
                                                player.transform.rotation.y*180/Mathf.PI  * 0.5f ,
                                                player.transform.rotation.z * cameraVibrationSpeed) ;
        }
        if(focus == 10){
            Vector3 swarmOffset = new Vector3(0, 6.74f, -10.9f);
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + swarmOffset;
            transform.rotation = Quaternion.Euler(30.112f + player.transform.rotation.x * cameraVibrationSpeed ,
                                                player.transform.rotation.y*180/Mathf.PI  * 0.5f ,
                                                player.transform.rotation.z * cameraVibrationSpeed) ;

        }
        else{
            // Camera follow first loyal wingman position
            transform.position = new Vector3(drones[focus-1].transform.position.x, drones[focus-1].transform.position.y, drones[focus-1].transform.position.z) + offset;
            transform.rotation = Quaternion.Euler(30.112f + drones[focus-1].transform.rotation.x * cameraVibrationSpeed ,
                                                drones[focus-1].transform.rotation.y*180/Mathf.PI  * 0.5f ,
                                                drones[focus-1].transform.rotation.z * cameraVibrationSpeed) ;
        }



    }
}
