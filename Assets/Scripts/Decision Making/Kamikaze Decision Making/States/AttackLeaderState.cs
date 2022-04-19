using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeaderState : BaseState
{
    public GameObject agent;
    public Vector3 targetPosition;
    public Transform targetTransform;
    public Vector3 currentPosition;
    public float speed = 1.0f;
    private KamikazeFSM _ksm;
    private DroneManager _dm;
    private Rigidbody leaderRb;

    public AttackLeaderState(string name, KamikazeFSM stateMachine, GameObject agent) : base(name, stateMachine, agent)
    {
        _ksm = stateMachine;
        this.agent = agent;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entering AttackLeaderState ");
        // get agent drone manager
        _dm = agent.GetComponent<DroneManager>();
        // get leader rigidbody
        leaderRb = _dm.leaderRb;
    }

    public override void CheckTransition()
    {
        base.CheckTransition();
        Debug.Log("AttackLeaderState Check Transition");

        currentPosition = agent.transform.position;
        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
        {
            stateMachine.ChangeState(_ksm.attackLoyalWingmanState);
        }

    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("AttackLeaderState  being Executed");
        Vector3 offset = new Vector3(0, 5 , 0);
        try{ // if leader exists 
            targetPosition = leaderRb.position + offset;
            _dm.GoTo(targetPosition);
        } // if leader does not exist, go back to goToPositionState
        catch{
            stateMachine.ChangeState(_ksm.goToPositionState);
            targetPosition = new Vector3(1, 10, 1);
            _dm.GoTo(targetPosition);
        }

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting GoToPosition State");
    }
}
