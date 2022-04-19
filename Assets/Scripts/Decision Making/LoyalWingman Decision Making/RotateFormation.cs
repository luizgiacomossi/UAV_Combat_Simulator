using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RotateFormation : Node
{
    GameObject agent;
    GameObject leader;
    LoyalWingmenManager lwm;
    ManualDroneManager mdm;
    public string nodeName;
    LoyalBT bt;

    public RotateFormation(GameObject agentGO, GameObject leaderGO) : base( "RotateFormation") {
        this.agent = agentGO;
        this.leader = leaderGO;
        this.nodeName = "RotateFormation";

        // GET LEADER MANUAL DRONE MANAGER
        mdm = leader.GetComponent<ManualDroneManager>();
        Debug.Log("Enter BT !! GoToFormation");
       // get loyal wingman manager
        lwm = agent.GetComponent<LoyalWingmenManager>();
        // get btree
        bt = agent.GetComponent<LoyalBT>();
     } // constructor

    
    public override NodeState Run(){
        base.SetName(nodeName);
        bt.currentBehaviorName = nodeName;
        lwm.offsetAngleFormation = lwm.offsetAngleFormation + 0.2f;

        return NodeState.SUCCESS;


    }



}
