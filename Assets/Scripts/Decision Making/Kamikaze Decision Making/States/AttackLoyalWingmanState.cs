using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLoyalWingmanState : BaseState
{
    public GameObject agent;
    public Vector3 targetPosition;
    public Transform targetTransform;
    public Vector3 currentPosition;
    public Vector3 initialPos;
    public float speed = 1.0f;

    // drones comopents used by the state
    private KamikazeFSM _ksm;
    private DroneManager _dm;
    // get loyal wingman rigidbodies list
    private List<Rigidbody> loyalRbs;

    // constructor
    public AttackLoyalWingmanState(string name, KamikazeFSM stateMachine, GameObject agent) : base(name, stateMachine, agent)
    {
        _ksm = stateMachine;
        this.agent = agent;
    }

    // on enter state
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Entering GoToPosition State");
        // get agent drone manager
        _dm = agent.GetComponent<DroneManager>();
        initialPos = agent.transform.position;
        // get loyal wingman rigidbodies list
        loyalRbs = _dm.loyalRbs;

        bool found = false;
        // select on loyal randomly from the list
        while (!found)
        {
            int index = Random.Range(0, loyalRbs.Count);
            targetTransform = loyalRbs[index].transform;
            found = !loyalRbs[index].GetComponent<DroneManager>().isDestroyed;
        }

    }

    public override void CheckTransition()
    {
        base.CheckTransition();
        currentPosition = agent.transform.position;

        if (Vector3.Distance(currentPosition, targetPosition) < 0.51f)
        {
            stateMachine.ChangeState(_ksm.attackLeaderState);
        }

    }

    public override void Execute()
    {
        base.Execute();
        targetPosition = targetTransform.position + new Vector3(0, 3, 0);
        _dm.GoTo(targetPosition);

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exiting GoToPosition State");
    }
}
