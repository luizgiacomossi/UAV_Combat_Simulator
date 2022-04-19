using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class LoyalBT : BTree
{
    public Transform[] waypoints;
    public GameObject agent;
    public GameObject leader;
    public string currentBehaviorName;

    // constructor
    public LoyalBT( GameObject agentGO ) : base() { 
        agent = agentGO;
        leader = GameObject.Find("Player");
    }

    protected override Node SetupTree(){
        Node root = new RotateFormation(agent, leader);

        return root;

    }

    protected override void FixedUpdate(){
        base.FixedUpdate();
        base.SetName(currentBehaviorName);
    }

   
}
