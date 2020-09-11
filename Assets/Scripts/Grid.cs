using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;

    private int[,] gridArray;

    public Grid(int _w, int _h, float _cs)
    {
        width = _w;
        height = _h;
        cellSize = _cs;

        gridArray = new int[width, height];

        for(int i = 0; i < gridArray.GetLength(0); i++)
        {
            for(int j = 0; j < gridArray.GetLength(1); j++)
            {
                gridArray[i, j] = 0;
            }
        }
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize;
    }

    public void SetValue(int x, int y, int value)
    {
        gridArray[x, y] = value;
    }

    public int GetValue(int x, int y)
    {
        return gridArray[x, y];
    }

    public int[,] GetGridArray()
    {
        return gridArray;
    }
}
