using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Monster : MonoBehaviour
{
    [SerializeField] float distance;

    [SerializeField] float killDistance;

    
    [SerializeField] public Transform player;

    [SerializeField] HideManager hideManager;

    string lastState;

    BTNode BTree;

    public void Start()
    {
        lastState = "Idle";
        Func<float> GetDistance = () => Vector2.Distance(transform.position,player.position);
        

        BTNode idleNode = new Sequence(new List<BTNode>{
            new ConditionNode(()=> GetDistance() > distance||hideManager.stateMachine.CurrentState is Hiding),
            new ActionNode(()=> idleMotion()) 
        });
        BTNode FollowNode = new Sequence(new List<BTNode>{
            new ConditionNode(()=> GetDistance() <= distance&&GetDistance()>killDistance&&hideManager.stateMachine.CurrentState is Running),
            new ActionNode(()=>TrackingPlayer())
        });
        BTNode KillNode = new Sequence(new List<BTNode>
        {
             new ConditionNode(()=> GetDistance() <=  killDistance&&hideManager.stateMachine.CurrentState is Running),
             new ActionNode(()=>KillPlayer())
        });

        BTree = new Selector(new List<BTNode>
        {
            idleNode,FollowNode,KillNode
        });
    }

    public void Update()
    {
         string current = GetCurrentStateName();

        // 상태가 바뀌었을 때만 코루틴 정지
        if (current != lastState)
        {
            var design = this as DesignMonster;
            var developer = this as SwMonster;
            if (design != null)
                design.StopActions();
            if(developer != null)
                developer.StopActions();

            lastState = current;
        }
         
        BTree.Evaluate();
    }

    public virtual BehaviourState idleMotion()
    {
        return BehaviourState.Success;
    }

    public virtual BehaviourState TrackingPlayer()
    {
        
        return BehaviourState.Running;
    }

    BehaviourState KillPlayer()
    {
        Debug.Log("플레이어 살해");
        return BehaviourState.Running;
    }

    string GetCurrentStateName()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= killDistance&&hideManager.stateMachine.CurrentState is Running) return "Kill";
        if (dist <= distance&&hideManager.stateMachine.CurrentState is Running) return "Follow";
        return "Idle";
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance); // 구 형태지만 평면에서 원처럼 보임

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killDistance);
    }

}