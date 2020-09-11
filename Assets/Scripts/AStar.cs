using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private Grid grid;
    private EnemyController enemy;
    private PlayerCharacter player;
    private Vector2Int source;
    private Vector2Int target;
    private Node[,] nodes;
    private List<Node> open;
    private Node current;

    private bool foundPlayer;

    public AStar(Grid _grid, EnemyController _enemy, PlayerCharacter _player)
    {
        grid = _grid;
        enemy = _enemy;
        player = _player;

        nodes = new Node[grid.GetGridArray().GetLength(0), grid.GetGridArray().GetLength(1)];
        open = new List<Node>();
        foundPlayer = false;


        Vector2Int enemyPosition = enemy.GetPositionOnGrid();
        current = new Node(enemyPosition.x, enemyPosition.y, 1, 0, 0, null);
        open.Insert(0, current);
        target = player.GetPositionOnGrid();
        Debug.Log(current.ToString());

        Iterate();
    }

    void Iterate()
    {
        current = open[0];
        open.RemoveAt(0);
        current.SetState(2);

        Node[] neighbours = SetNeighbours(current.GetPosition().x, current.GetPosition().y);

    }

    Node[] SetNeighbours(int x, int y)
    {
        Node[] neighbours = new Node[8];

        if (x + 1 < grid.GetGridArray().GetLength(0))
        {
           neighbours[0] = CheckNodeAt(x + 1, y);
        }

        return neighbours;
    }

    Node CheckNodeAt(int x, int y)
    {
        if(grid.GetGridArray()[x, y] == 1) // if it's an obstacle
        {
            return null;
        }

        if (nodes[x, y] == null) // if it's not open yet
        {
            nodes[x, y] = new Node(x, y, 1, 0, 0, current);
            CalculateCost(nodes[x, y], current);

            return nodes[x, y];
        }

        if (nodes[x, y].GetState() != 2 && nodes[x, y].GetGCost() > CalculateGCost(nodes[x,y], current)) // if not closed
        {
            CalculateCost(nodes[x, y], current);
            nodes[x, y].SetParent(current);

            return nodes[x, y];
        }

        return null;
    }

    int CalculateGCost(Node n, Node src)
    {
        int g_cost;

        if (n.GetPosition().x != src.GetPosition().x && n.GetPosition().y != src.GetPosition().y) // diagonal movement
        {
            g_cost = n.GetParent().GetGCost() + 14;
        }
        else
        {
            g_cost = n.GetParent().GetGCost() + 10;
        }

        return g_cost;
    }

    void CalculateCost(Node n, Node src)
    {
        int g_cost;
        int h_cost;
        int f_cost;

        g_cost = CalculateGCost(n, src);

        int min, max;
        int Xp, Yp;
        Yp = Mathf.Abs(n.GetPosition().y - target.y);
        Xp = Mathf.Abs(n.GetPosition().x - target.x);
        min = Mathf.Min(Xp, Yp);
        max = Mathf.Max(Xp, Yp);

        h_cost = (min * 14) + ((max - min) * 10);
        f_cost = g_cost + h_cost;

        n.SetGCost(g_cost);
        n.SetHCost(h_cost);
        n.SetFCost(f_cost);

        Debug.Log(g_cost + " " + h_cost + " " + f_cost);
    }
}
