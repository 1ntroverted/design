using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignMonster : Monster
{

    [SerializeField] Agrid agrid;

    [SerializeField] Transform[] wayPoints;

    [Header("Move")]
    [SerializeField] float speed = 3.5f;        // 유닛/초
    [SerializeField] float arriveRadius = 0.05f;// 도착 판정
    int i=0;
     PathFinding pathFinding;
     SpriteRenderer spriteRenderer;
     bool isMoving;

     Coroutine move;
    void Awake()
    {
        pathFinding = new PathFinding(agrid);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void StopActions()
    {
        if(move != null)
        {
            StopCoroutine(move);
            move = null;
        }
        isMoving = false;
    }
    
    public override BehaviourState idleMotion()
    {
        
        
        if(wayPoints ==null) return BehaviourState.Failure;
        if(isMoving) return BehaviourState.Running;
        Transform targetWp = wayPoints[i];
        List<Anode> path = pathFinding.FindPath(transform.position,targetWp.position);
        
        isMoving = true;
        move = StartCoroutine(FollowPath(path));

        

        return BehaviourState.Running;
    }

    public override BehaviourState TrackingPlayer()
    {
        
        if(isMoving) return BehaviourState.Running;
        
        List<Anode> path = pathFinding.FindPath(transform.position,player.position);
        
        isMoving = true;
         move = StartCoroutine(FollowPath(path));

        

        return BehaviourState.Running;
    }

    IEnumerator FollowPath(List<Anode> path)
    {
        isMoving = true;
        // 각 노드까지 프레임마다 이동
        for (int idx = 0; idx < path.Count; idx++)
        {
            Vector3 target = path[idx].nodePos;

            if (target.x > transform.position.x)
            {
                spriteRenderer.flipX = true;
            } 
            if (target.x < transform.position.x)
            {
                spriteRenderer.flipX = false;
            } 
            // 2D라면 z 고정(씬에 맞춰 조정)
            target.z = transform.position.z;

            // 해당 노드에 도착할 때까지 MoveTowards
            while ((transform.position - target).sqrMagnitude > arriveRadius * arriveRadius)
            {
                Vector3 next = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                transform.position = next;
                yield return null;
            }
        }
        isMoving = false;
        i = (i + 1) % wayPoints.Length;
    }
}