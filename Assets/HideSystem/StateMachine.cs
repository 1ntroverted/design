using UnityEngine;

public class StateMachine
{
    public State CurrentState{ get; private set;}
    public StateMachine(State initialState)
    {
        CurrentState = initialState;

        CurrentState.OnEnter();
    }

    public void StateChange(State newState)
    {
        if (newState == CurrentState) return;

        
        CurrentState?.OnExit();
        

        CurrentState = newState;
        CurrentState.OnEnter();
    }

    public void UpdateState()
    {
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }
}
