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
    private int[,] visited;
    private float[,] dist;
    private Vector2Int[,] parent;

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


        for(int i = 0; i < grid.GetGridArray().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
            {
                visited[i, j] = 0;
                dist[i, j] = Mathf.Infinity;
            }
        }
    }


}
