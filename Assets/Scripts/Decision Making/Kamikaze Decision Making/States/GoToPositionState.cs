using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPositionState : BaseState
{
    public GameObject agent;
    public Vector3 targetPosition;
    public Transform targetTransform;
    public Vector3 currentPosition;
    public Vector3 initialPos;
    public float speed = 1.0f;
    private KamikazeFSM _ksm;
    private DroneManager _dm;


    public GoToPositionState(string name, KamikazeFSM stateMachine, GameObject agent) : base(name, stateMachine, agent)
    {
        _ksm = stateMachine;
        this.agent = agent;
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entering GoToPosition State");
        // get agent drone manager
        _dm = agent.GetComponent<DroneManager>();
        initialPos = agent.transform.position;

    }

    public override void CheckTransition()
    {
        base.CheckTransition();
        //Debug.Log("GoToPosition State Check Transition");
        currentPosition = agent.transform.position;
        if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
        {
            stateMachine.ChangeState(_ksm.attackLeaderState);
        }

    }

    public override void Execute()
    {
        base.Execute();
        //Debug.Log("GoToPosition State being Executed");

        targetPosition = new Vector3(initialPos.x, initialPos.y , initialPos.z) + new Vector3(0, 10, 0);
        _dm.GoTo(targetPosition);

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting GoToPosition State");
    }
}
