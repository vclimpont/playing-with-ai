using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int x, y;
    private int state; // 0 not evaluated yet, 1 open, 2 closed
    private int cost;
    private Node parent;

    public Node(int _x, int _y, int _state, int _cost, Node _parent)
    {
        x = _x;
        y = _y;
        state = _state;
        cost = _cost;
        parent = _parent;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }

    public void SetState(int _state)
    {
        state = _state;
    }

    public int GetState()
    {
        return state;
    }

    public void SetCost(int _cost)
    {
        cost = _cost;
    }

    public int GetCost()
    {
        return cost;
    }

    public void SetParent(Node _parent)
    {
        parent = _parent;
    }

    public Node GetParent()
    {
        return parent;
    }

    override
    public string ToString()
    {
        return x + " " + y + " " + state + " " + cost + " ";
    }
}
