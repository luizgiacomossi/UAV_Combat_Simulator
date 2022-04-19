using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Generic State to be inherited by all states
public class BaseState 
{
    public string stateName;
    public StateMachine stateMachine;
    public GameObject agent;

    public BaseState(string name, StateMachine stateMachine, GameObject agent)
    {
        this.stateName = name;
        this.stateMachine = stateMachine;
        this.agent = agent;
    }

    public virtual void Enter() { }
    public virtual void CheckTransition() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}
