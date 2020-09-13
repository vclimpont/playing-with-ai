using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    private Grid grid;
    private EnemyController enemy;
    private PlayerCharacter player;
    private Vector2Int source;
    private Vector2Int target;
    private Vector2Int current;
    private int[,] visited;
    private float[,] dist;
    private Vector2Int[,] parent;
    private List<Vector2Int> pathList;
    private Vector2Int[] path;

    public Dijkstra(Grid _grid, EnemyController _enemy, PlayerCharacter _player)
    {
        grid = _grid;
        enemy = _enemy;
        player = _player;

        source = enemy.GetPositionOnGrid();
        target = player.GetPositionOnGrid();

        visited = new int[grid.GetGridArray().GetLength(0), grid.GetGridArray().GetLength(1)];
        dist = new float[grid.GetGridArray().GetLength(0), grid.GetGridArray().GetLength(1)];
        parent = new Vector2Int[grid.GetGridArray().GetLength(0), grid.GetGridArray().GetLength(1)];
        pathList = new List<Vector2Int>();


        for(int i = 0; i < grid.GetGridArray().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
            {
                visited[i, j] = grid.GetGridArray()[i, j]; // 0 if not obstacle, 1 overwhise
                dist[i, j] = Mathf.Infinity;
            }
        }

        current = source;
        visited[current.x, current.y] = 1;
        dist[current.x, current.y] = 0;

        while(current != target)
        {
            Iterate();
        }

        RetracePath();
    }

    void Iterate()
    {
        ExaminateNeighbours(current.x, current.y);
        current = GetMinUnivisited();
        visited[current.x, current.y] = 1;
    }

    void RetracePath()
    {
        Vector2Int tmpPos = target;

        while(tmpPos != source)
        {
            pathList.Insert(0, tmpPos);
            tmpPos = parent[tmpPos.x, tmpPos.y];
        }

        path = new Vector2Int[pathList.Count];
        for(int i = 0; i < pathList.Count; i++)
        {
            path[i] = pathList[i];
        }
    }

    Vector2Int GetMinUnivisited()
    {
        int minX = 0;
        int minY = 0;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < grid.GetGridArray().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
            {
                if(visited[i,j] == 0 && dist[i,j] < minDist)
                {
                    minX = i;
                    minY = j;
                    minDist = dist[i, j];
                }
            }
        }

        return new Vector2Int(minX, minY);
    }

    void ExaminateNeighbours(int x, int y)
    {

        if (x + 1 < grid.GetGridArray().GetLength(0))
        {
            CheckNeighbourAt(x + 1, y, 10f);

            if (y + 1 < grid.GetGridArray().GetLength(1))
            {
                CheckNeighbourAt(x + 1, y + 1, 14f);
            }
            if (y - 1 >= 0)
            {
                CheckNeighbourAt(x + 1, y - 1, 14f);
            }
        }

        if (x - 1 >= 0)
        {
            CheckNeighbourAt(x - 1, y, 10f);

            if (y + 1 < grid.GetGridArray().GetLength(1))
            {
                CheckNeighbourAt(x - 1, y + 1, 14f);
            }
            if (y - 1 >= 0)
            {
                CheckNeighbourAt(x - 1, y - 1, 14f);
            }
        }

        if (y + 1 < grid.GetGridArray().GetLength(1))
        {
            CheckNeighbourAt(x, y + 1, 10f);
        }

        if (y - 1 >= 0)
        {
            CheckNeighbourAt(x, y - 1, 10f);
        }
    }

    void CheckNeighbourAt(int x, int y, float cost)
    {
        if (visited[x, y] == 1)
        {
            return;
        }

        float distFromSource = dist[current.x, current.y] + cost;

        if(distFromSource < dist[x, y])
        {
            dist[x, y] = distFromSource;
            parent[x, y] = current;
        }
    }

    void DisplayDistAndParent()
    {
        for (int i = 0; i < grid.GetGridArray().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
            {
                if(dist[i, j] < Mathf.Infinity)
                {
                    Debug.Log(new Vector2Int(i,j) + " " + dist[i, j] + " " + parent[i,j]);
                }
            }
        }
    }

    void DisplayPath()
    {
        string s = "";

        for(int i = 0; i < path.Length; i++)
        {
            s += path[i] + " ";  
        }

        Debug.Log(s);
    }

    public Vector2Int[] GetPath()
    {
        return path;
    }

}
