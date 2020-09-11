using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int x, y;
    private int state; // 0 not evaluated yet, 1 open, 2 closed
    private int g_cost;
    private int h_cost;
    private int f_cost;
    private Node parent;

    public Node(int _x, int _y, int _state, int _g_cost, int _h_cost, Node _parent)
    {
        x = _x;
        y = _y;
        state = _state;
        g_cost = _g_cost;
        h_cost = _h_cost;
        f_cost = g_cost + h_cost;
        parent = _parent;
    }

    public Vector2Int GetPosition()
    {
        return new Vector2Int(x, y);
    }

    public void SetState(int _state)
    {
        state = _state;
    }

    public int GetState()
    {
        return state;
    }

    public void SetGCost(int _g_cost)
    {
        g_cost = _g_cost;
    }

    public int GetGCost()
    {
        return g_cost;
    }

    public void SetHCost(int _h_cost)
    {
        h_cost = _h_cost;
    }

    public int GetHCost()
    {
        return h_cost;
    }

    public void SetFCost(int _f_cost)
    {
        f_cost = _f_cost;
    }

    public int GetFCost()
    {
        return f_cost;
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
        return x + " " + y + " " + state + " " + f_cost + " ";
    }
}
