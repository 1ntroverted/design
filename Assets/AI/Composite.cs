using System.Collections.Generic;
using UnityEngine;

public class Selector : BTNode //자식 노드중 하나라도 성공이나 실행중이면 그 상태 반환
{
    private List<BTNode> childNode;
    public Selector(List<BTNode> childNode) => this.childNode = childNode;

    public override BehaviourState Evaluate()
    {
        foreach (BTNode child in childNode)
        {
            switch (child.Evaluate())
            {
                case BehaviourState.Success:
                    return BehaviourState.Success;
                case BehaviourState.Running:
                    return BehaviourState.Running;
            }
        }
        return BehaviourState.Failure;
    }
}



public class Sequence : BTNode //자식 노드 중 하나라도 실패하면 실패 반환
{
    private List<BTNode> childNode;
    public Sequence(List<BTNode> childNode) => this.childNode = childNode;

    public override BehaviourState Evaluate()
    {
        
        foreach (BTNode child in childNode)
        {
            switch (child.Evaluate())
            {
                case BehaviourState.Failure:
                    return BehaviourState.Failure;
                case BehaviourState.Success:
                    continue;
                case BehaviourState.Running:
                    return BehaviourState.Running;
            }
        }
        return BehaviourState.Success;
    }
}