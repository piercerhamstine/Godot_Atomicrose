using System;
using System.Collections.Generic;
using System.IO;
using Godot;

public partial class StateMachine
{
    public delegate void State();

    Dictionary<string, State> validStates;
    State currentState;

    public StateMachine(){
        validStates = new();
    }

    public void SetInitialState(State state){
        if(validStates.ContainsKey(state.Method.Name))
            currentState = state;
    }

    public void ChangeState(State stateToChangeTo){
        if(validStates.ContainsKey(stateToChangeTo.Method.Name)){
            currentState = stateToChangeTo;
        }
    }

    public void AddState(State state){
        validStates.Add(state.Method.Name, state);
    }

    public void Update(){
        currentState?.Invoke();
    }
}
