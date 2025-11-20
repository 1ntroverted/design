using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinding))]
public class PatrolAi : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] Transform[] wayPoints;
    [SerializeField] bool loop = true;          // true면 마지막 다음에 0으로

    [Header("Move")]
    [SerializeField] float speed = 3.5f;        // 유닛/초
    [SerializeField] float arriveRadius = 0.05f;// 도착 판정

    PathFinding finder;
    bool isPatrolling;

    SpriteRenderer spriteRenderer;


    void Awake()
    {
        finder = GetComponent<PathFinding>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (!isPatrolling && wayPoints != null && wayPoints.Length > 0)
            StartCoroutine(Patrol());
    }
    IEnumerator Patrol()
    {
        isPatrolling = true;

        int i = 0;
        while (true)
        {
            Transform targetWp = wayPoints[i];
            if (targetWp == null) yield break;

            // ★ A*로 현재 위치 → 목표 웨이포인트 경로 받기
            List<Anode> path = finder.FindPath(transform.position, targetWp.position);
            if (path != null && path.Count > 0)
            {
                // 이 경로를 따라 한 칸씩 이동
                yield return StartCoroutine(FollowPath(path));
            }
            else
            {
                // 경로 없으면 잠깐 쉬고 재시도(막힘 대비)
                yield return new WaitForSeconds(0.25f);
            }

            // 다음 웨이포인트 인덱스
            if (loop)
                i = (i + 1) % wayPoints.Length;
            else
            {
                i = Mathf.Min(i + 1, wayPoints.Length - 1);
                if (i == wayPoints.Length - 1) break; // 열린 경로면 끝에서 정지
            }
        }

        isPatrolling = false;
    }

    IEnumerator FollowPath(List<Anode> path)
    {
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
    }

}