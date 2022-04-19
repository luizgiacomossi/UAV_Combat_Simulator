using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class JoinFormation : Node
{
    GameObject agent;
    GameObject leader;
    LoyalWingmenManager lwm;
    ManualDroneManager mdm;
    public string nodeName;
    FormationBT bt;

    public JoinFormation(GameObject agentGO, GameObject leaderGO) {
        this.agent = agentGO;
        this.leader = leaderGO;
        this.nodeName = "JoinFormation";

        // GET LEADER MANUAL DRONE MANAGER
        mdm = leader.GetComponent<ManualDroneManager>();
       // get loyal wingman manager
        lwm = agent.GetComponent<LoyalWingmenManager>();
        // get btree
        bt = agent.GetComponent<FormationBT>();
        Debug.Log("Enter BT !! JoinFormation");

     } // constructor

    
    public override NodeState Run(){
        base.SetName(nodeName);
        bt.currentBehaviorName = nodeName;
        // Enter here logic to join formation
        
        return NodeState.SUCCESS;


    }



}
