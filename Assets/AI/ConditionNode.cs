using UnityEngine;
using System;
public class ConditionNode : BTNode
{

    public Func<bool> condition;

    public ConditionNode(Func<bool> condition) => this.condition = condition;

    
    public override BehaviourState Evaluate()
    {
        return condition() ? BehaviourState.Success : BehaviourState.Failure;
    }
}