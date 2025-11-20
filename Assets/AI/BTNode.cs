using UnityEngine;
public enum BehaviourState { Success, Running, Failure }
public abstract class BTNode
{
    
    public abstract BehaviourState Evaluate();
    
}
