using System;
using UnityEngine;


public class ActionNode : BTNode
{
    private Func<BehaviourState> action;

    public ActionNode(Func<BehaviourState> action) => this.action = action;


    public override BehaviourState Evaluate()
    {
        return action.Invoke();
    }
}