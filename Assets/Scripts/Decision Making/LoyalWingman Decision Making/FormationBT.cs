using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class FormationBT : BTree
{
    public GameObject agent;
    public GameObject leader;
    public string currentBehaviorName ;

    // on enter
   protected override void Start(){
        base.Start();
        currentBehaviorName = "Teste Formação";

    }

    protected override Node SetupTree(){
        Node root = new JoinFormation(agent, leader);
        Debug.Log("ENTREI SETUP " );

        return root;

    }

    protected override void FixedUpdate(){
        base.FixedUpdate();
        base.SetName(currentBehaviorName);
    }

   
}
