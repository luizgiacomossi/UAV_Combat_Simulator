using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeFSM : StateMachine
{
    public GameObject agent;
    public GoToPositionState goToPositionState;
    public AttackLeaderState attackLeaderState;
    public AttackLoyalWingmanState attackLoyalWingmanState;
    public string currentStateName;

    // awake is called before start
    private void Awake() {
        agent = gameObject;
        goToPositionState = new GoToPositionState("GoToPositionState", this, agent);
        attackLeaderState = new AttackLeaderState("AttackLeaderState", this, agent);
        attackLoyalWingmanState = new AttackLoyalWingmanState("AttackLoyalWingmanState", this, agent);
        currentStateName = goToPositionState.stateName;
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        // get the game object that is executing this script
        agent = gameObject;
    }

    // override the base class's getinitialstate function
    protected override BaseState GetInitialState()
    {
        return goToPositionState;
    }



}
