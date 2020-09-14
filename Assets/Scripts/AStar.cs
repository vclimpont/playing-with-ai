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
    private Vector2Int[] path;
    private List<Node> open;
    private List<Node> pathNodes;
    private Node current;


    public AStar(Grid _grid, EnemyController _enemy, PlayerCharacter _player)
    {
        grid = _grid;
        enemy = _enemy;
        player = _player;

        nodes = new Node[grid.GetGridArray().GetLength(0), grid.GetGridArray().GetLength(1)];
        open = new List<Node>();
        pathNodes = new List<Node>();


        source = enemy.GetPositionOnGrid();
        current = new Node(source.x, source.y, 1, 0, 0, null);    // set current to enemy position
        open.Insert(0, current);                                                // add current as first element of open list
        target = player.GetPositionOnGrid();                                    // target is the player

        while(current.GetPosition() != target)                                  // while we didn't reach the player's position
        {
            Iterate();
        }

        RetracePath();

    }

    void RetracePath()
    {
        Node pointer = current;
        while(pointer.GetPosition() != source)                                  // retrace positions until the source (the enemy position)
        {
            pathNodes.Insert(0, pointer);
            pointer = pointer.GetParent();
        }

        path = new Vector2Int[pathNodes.Count];

        for (int i = 0; i < pathNodes.Count; i++)                               // build the path
        {
            path[i] = pathNodes[i].GetPosition();
        }
    }

    void Iterate()
    {
        current = open[0];
        open.RemoveAt(0);                                                       // remove current from open list
        current.SetState(2);                                                    // set current to closed

        Node[] neighbours = SetNeighbours(current.GetPosition().x, current.GetPosition().y);

        SortAndAddOpenNeighbours(neighbours);
    }

    void SortAndAddOpenNeighbours(Node[] neighbours)
    {
        for(int i = 0; i < 8; i++)                                              // for each neighbour
        {
            if(neighbours[i] != null)
            {
                if(open.Count == 0)
                {
                    open.Insert(0, neighbours[i]);
                }
                else
                {
                    if (open.Contains(neighbours[i]))            // avoid clones
                    {
                        open.Remove(neighbours[i]);
                    }

                    int k = 0;

                    while(k < open.Count && neighbours[i].GetFCost() > open[k].GetFCost())      // search for the shortest neighbour
                    {
                        k++;
                    }

                    if(k < open.Count)
                    {
                        open.Insert(k, neighbours[i]);
                    }
                    else
                    {
                        open.Add(neighbours[i]);
                    }
                }
            }
        }

    }

    Node[] SetNeighbours(int x, int y)
    {
        Node[] neighbours = new Node[8];

        if (x + 1 < grid.GetGridArray().GetLength(0))                            // check every neighbours available (8 max)
        {
           neighbours[0] = CheckNodeAt(x + 1, y);

            if (y + 1 < grid.GetGridArray().GetLength(1))
            {
                neighbours[1] = CheckNodeAt(x + 1, y + 1);
            }
            if (y - 1 >= 0)
            {
                neighbours[7] = CheckNodeAt(x + 1, y - 1);
            }
        }

        if (x - 1 >= 0)
        {
            neighbours[4] = CheckNodeAt(x - 1, y);

            if (y + 1 < grid.GetGridArray().GetLength(1))
            {
                neighbours[3] = CheckNodeAt(x - 1, y + 1);
            }
            if (y - 1 >= 0)
            {
                neighbours[5] = CheckNodeAt(x - 1, y - 1);
            }
        }

        if (y + 1 < grid.GetGridArray().GetLength(1))
        {
            neighbours[2] = CheckNodeAt(x, y + 1);
        }

        if (y - 1 >= 0)
        {
            neighbours[6] = CheckNodeAt(x, y - 1);
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
            g_cost = src.GetGCost() + 14;           // sqrt(1 + 1) * 10
        }
        else
        {
            g_cost = src.GetGCost() + 10;           // 1 * 10
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
    }

    public Vector2Int[] GetPath()
    {
        return path;
    }
}
