using System.Collections;
using UnityEngine;

public class StateMachine
{
    public IState currentState;
    public StateMachine(IState initialState)
    {
        currentState = initialState;

        currentState.OnEnter();
    }

    public void StateChange(IState newState)
    {
        if (newState == currentState) return;

        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;
        currentState.OnEnter();
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }

    
}
