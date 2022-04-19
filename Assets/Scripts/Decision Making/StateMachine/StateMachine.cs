using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;
    public string currenteStateName;
    
    // Start is called before the first frame update
    public void Start()
    {
        currentState = GetInitialState();
        currenteStateName = currentState.stateName;
        if(currentState != null){
            currentState.Enter();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currenteStateName = currentState.stateName;
        // if the current state is not null
        if (currentState != null)
        {
            currentState.CheckTransition();
        }
    }

    private void LateUpdate() {
        if (currentState != null)
        {
            currentState.Execute();
        }   
    }

    public void ChangeState(BaseState newState)
    {
        // if the current state is not null
        if (currentState != null)
        {
            // exit current state
            currentState.Exit();
        }

        // set current state to new state
        currentState = newState;

        // enter new state
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    private void OnGUI(){
        string content = currentState != null ? currentState.stateName : " (no current state) ";
        GUILayout.Label($"<color='black'><size>Current State:{content}</size></color>");

    }

}
