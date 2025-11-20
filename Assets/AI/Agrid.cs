using System;
using System.Collections.Generic;
using UnityEngine;

public class Agrid : MonoBehaviour
{
    public LayerMask unWalkableMask;
    public Vector2 worldSize;
    public float nodeRadius;

    Anode[,] grid;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    public List<Anode> path;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(worldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(worldSize.y / nodeDiameter);
        CreateGrid();
    }

    void Update()
    {
        UpdatePath();
    }

    void UpdatePath()
    {
        Vector3 worldBottemLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.up * worldSize.y / 2;
        Vector2 worldPoint;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                worldPoint = worldBottemLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unWalkableMask));
                grid[x, y].walkable = walkable;
            }
        }
    }

    void CreateGrid()
    {
        grid = new Anode[gridSizeX, gridSizeY];
        Vector3 worldBottemLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.up * worldSize.y / 2;
        Vector2 worldPoint;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                worldPoint = worldBottemLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unWalkableMask));
                grid[x, y] = new Anode(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Anode> GetNeighborNode(Anode node)
    {
        List<Anode> neighbors = new List<Anode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int cx = node.gridX + x;
                int cy = node.gridY + y;

                if (cx >= 0 && cx < gridSizeX && cy >= 0 && cy < gridSizeY)
                {
                    neighbors.Add(grid[cx, cy]);
                }
            }

        }
        return neighbors;
    }

    public Anode GetNodePosition(Vector3 worldPos)
    {
        // 월드→로컬
        Vector3 local = transform.InverseTransformPoint(worldPos);

        // 로컬 기준 -worldSize/2 ~ +worldSize/2 범위를 0~1로
        float percentX = (local.x + worldSize.x * 0.5f) / worldSize.x;
        float percentY = (local.y + worldSize.y * 0.5f) / worldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);


        int x = Mathf.Clamp(Mathf.FloorToInt(percentX * gridSizeX), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt(percentY * gridSizeY), 0, gridSizeY - 1);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, worldSize.y));
        if (grid != null)
        {
            foreach (Anode n in grid)
            {
                Gizmos.color = n.walkable ? Color.white : Color.red;


                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.green;
                    }
                }
                Gizmos.DrawCube(n.nodePos, new Vector3(nodeDiameter - 0.1f, nodeDiameter - 0.1f, 0.1f));
            }
        }
    }

    



}