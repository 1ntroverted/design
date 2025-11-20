using UnityEngine;

public class Anode
{
    public bool walkable;
    public Vector3 nodePos;

    public int gridX;
    public int gridY;

    public int gCost;//시작점 부터 현재노드까지 걸린 비용
    public int hCost;//현재 노드부터 끝점 까지의 예상 비용

    public Anode parentNode;

    public Anode(bool nWalk, Vector3 pos, int gx, int gy)
    {
        walkable = nWalk;
        nodePos = pos;
        gridX = gx;
        gridY = gy;
    }
    public int fCost
    {
        get{ return hCost + gCost; }//전체 비용
    }
}
