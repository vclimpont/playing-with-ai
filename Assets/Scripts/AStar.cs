using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private Grid grid;
    private EnemyController enemy;
    private PlayerCharacter player;
    private Node[,] nodes;
    private List<Node> open;
    private Node current;

    public AStar(Grid _grid, EnemyController _enemy, PlayerCharacter _player)
    {
        grid = _grid;
        enemy = _enemy;
        player = _player;

        nodes = new Node[grid.GetGridArray().GetLength(0), grid.GetGridArray().GetLength(1)];
        open = new List<Node>();


        Vector2 enemyPosition = enemy.GetPositionOnGrid();
        current = new Node((int)enemyPosition.x, (int)enemyPosition.y, 1, 0, null);
        open.Insert(0, current);
        Debug.Log(current.ToString());
    }
}
