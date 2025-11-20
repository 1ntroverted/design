using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    
    

    Agrid grid;
    
    public PathFinding(Agrid grid) => this.grid = grid;
    
    

    public List<Anode> FindPath(Vector3 startpos, Vector3 endPos)
    {
        Anode startNode = grid.GetNodePosition(startpos);
        Anode targetnode = grid.GetNodePosition(endPos);

        List<Anode> openList = new List<Anode>();
        HashSet<Anode> closedList = new HashSet<Anode>();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Anode currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost || (openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost))
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == targetnode)
            {

                return trackingPath(startNode, targetnode);

            }

            foreach (Anode n in grid.GetNeighborNode(currentNode))
            {
                if (!n.walkable || closedList.Contains(n)) continue;

                int currentToNeighborCost = currentNode.gCost + GetDistanceCost(currentNode, n);
                if (currentToNeighborCost < n.gCost || !openList.Contains(n))
                {
                    n.gCost = currentToNeighborCost;
                    n.hCost = GetDistanceCost(n, targetnode);
                    n.parentNode = currentNode;

                    if (!openList.Contains(n))
                    {
                        openList.Add(n);
                    }
                }


            }
        }
        return null;//목표 지점까지의 길을 찾지 못함
    }

    List<Anode> trackingPath(Anode startNode, Anode endNode)
    {
        List<Anode> path = new List<Anode>();
        Anode currentNode = endNode;
        
        
        while (currentNode != startNode)
        {
            
            

            path.Add(currentNode);

            currentNode = currentNode.parentNode;//맨 끝부터 부모 노드로 계속 올라감
            
        }
        
        path.Reverse();//최종 경로는 끝->시작으로 저장 되므로, 뒤집어서 시작->끝 경로로 만듬
        grid.path = path;
        return path;
        
    }
    

    int GetDistanceCost(Anode nodeA, Anode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);   
    }
}