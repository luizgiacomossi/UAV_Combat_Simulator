using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoyalWingmenManager : MonoBehaviour
{
    // This class is responsible to manage the commands to the loyal wingmen 
    // creates a list of loyal wingman rigidbodies
    public GameObject[] loyalWingmen;
    public List<Rigidbody> loyalWingmenRb;
    public List<Rigidbody> loyalWingmenInFormation;
    public float radiusFormation; // radius of the formation

    public List<Vector3> loyalWingmenPos; // list of positions of the wingmen in formation

    public Rigidbody leaderRb;
    public Vector3 leaderPos;
    public float stepAngle;
    public float offsetAngleFormation;

    // Start is called before the first frame update
    void Start() {
        // gets all loyal wingman rigidbodies
        loyalWingmen = GameObject.FindGameObjectsWithTag("LoyalWingman");
        foreach (GameObject wingman in loyalWingmen)
        {
            loyalWingmenRb.Add(wingman.GetComponent<Rigidbody>());
            loyalWingmenPos.Add(new Vector3(0, 0, 0));
        }
        // gets the leader rigid body
        leaderRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update(){
        leaderPos = leaderRb.position;
        int num_lw = loyalWingmenInFormation.Count;
        if(num_lw > 0){

        }
        stepAngle = 360 / num_lw * Mathf.PI / 180;

    }

    void FixedUpdate() {
        CalculatePositionsInFormation();
        // check loyal wingmen that still alive
        CheckLoyalAlive();
        // send coordinates in formation to the wingmen
        SendCoordinatesToWingmen();
    }

    private void CalculatePositionsInFormation(){
        // Define formation coordinates
        Vector3[] positions = new Vector3[loyalWingmenInFormation.Count];
        for (int i = 0; i < loyalWingmenInFormation.Count; i++)
        {   
            float x = radiusFormation * Mathf.Cos(stepAngle * i + offsetAngleFormation * Mathf.PI/180 ) + leaderPos.x;
            float y = leaderPos.y;
            float z = radiusFormation * Mathf.Sin(stepAngle * i + offsetAngleFormation * Mathf.PI/180 ) + leaderPos.z;
            positions[i] = new Vector3(x, y, z);
            loyalWingmenPos[i] = positions[i];
        }

    }

    // sends a command to all loyal wingmen to move to a given position
    void SendCoordinates(int id, Vector3 position ) {
        // get the drone manager of the drone with ID id
        DroneManager droneManager = loyalWingmenInFormation[id].GetComponent<DroneManager>();
        //Go to the position
        droneManager.GoTo(position);
    }

    // sends a command to all loyal wingmen to move to a given position
    void SendCoordinatesToWingmen(){
        // sends a command to all wingmen to move to the positions in the list
        for (int i = 0; i < loyalWingmenInFormation.Count; i++)
        {
            SendCoordinates(i, loyalWingmenPos[i]);
        }
    }

    private void CheckLoyalAlive(){
        // check if any of the loyal wingmen is dead
        loyalWingmenInFormation.Clear();
        for (int i = 0; i < loyalWingmen.Length; i++)
        {
            if (loyalWingmen[i].GetComponent<DroneManager>().GetisDestroyed()){
                // if the wingman is dead, remove it from the list of wingmen in formation
                loyalWingmenInFormation.Remove(loyalWingmenRb[i]);
            }
            else{
                // if the wingman is alive, add it to the list of wingmen in formation
                loyalWingmenInFormation.Add(loyalWingmenRb[i]);
            }
        }

    }

    public void JoinFormation(Rigidbody wingman){
        // add a wingman to the list of wingmen in formation
        loyalWingmenInFormation.Add(wingman);
    }

    public void JoinFormation(GameObject wingman){
        // add a wingman to the list of wingmen in formation
        loyalWingmenInFormation.Add(wingman.GetComponent<Rigidbody>());
    }


    public void LeaveFormation(Rigidbody wingman){
        // remove a wingman from the list of wingmen in formation
        loyalWingmenInFormation.Remove(wingman);
    }


    // ==== Usefull Methods ====
    public void LeaveFormation(int id){
        // remove a wingman from the list of wingmen in formation
        loyalWingmenInFormation.Remove(loyalWingmenRb[id]);
    }

    public void LeaveFormation(GameObject wingman){
        // remove a wingman from the list of wingmen in formation
        for (int i = 0; i < loyalWingmen.Length; i++)
        {
            if (loyalWingmen[i] == wingman){
                LeaveFormation(i);
            }
        }
    }

    public void RemoveAllLoyalWingmen(){
        // remove all wingmen from the list of wingmen in formation
        loyalWingmenInFormation.Clear();
    }

    public void AddAllLoyalWingmen(){
        // add all wingmen to the list of wingmen in formation
        for (int i = 0; i < loyalWingmen.Length; i++)
        {
            JoinFormation(loyalWingmenRb[i]);
        }
    }

    public void SetLeader(Rigidbody leader){
        // set the leader of the wingmen
        leaderRb = leader;
    }

    public void SetLeader(GameObject leader){
        // set the leader of the wingmen
        leaderRb = leader.GetComponent<Rigidbody>();
    }

    public void SetLeader(int id){
        // set the leader of the wingmen
        leaderRb = loyalWingmenRb[id];
    }

    public Rigidbody GetLeader(){
        // get the leader of the wingmen
        return leaderRb;
    }

    public List<Rigidbody> GetLoyalWingmen(){
        // get the list of wingmen in formation
        return loyalWingmenInFormation;
    }

    public List<Vector3> GetLoyalWingmenPos(){
        // get the list of wingmen positions in formation
        return loyalWingmenPos;
    }

    public int GetLoyalWingmenCount(){
        // get the number of wingmen in formation
        return loyalWingmenInFormation.Count;
    }

    public void SetRadiusFormation(float radius){
        // set the radius of the formation
        radiusFormation = radius;
    }

    public void SetOffsetAngleFormation(float offset){
        // set the offset angle of the formation
        offsetAngleFormation = offset;
    }

    public void SetStepAngle(float step){
        // set the step angle of the formation
        stepAngle = step;
    }

    public void SetLoyalWingmen(GameObject[] wingmen){
        // set the list of wingmen in formation
        loyalWingmen = wingmen;
        for (int i = 0; i < wingmen.Length; i++)
        {
            loyalWingmenRb[i] = wingmen[i].GetComponent<Rigidbody>();
        }
    }





}
