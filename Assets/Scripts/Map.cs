using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int width;
    public int height;
    public float cellSize;

    private Grid grid = new Grid(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(width, height, cellSize);
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < grid.GetGridArray().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
            {
                Gizmos.DrawCube(grid.GetWorldPosition(i, j), new Vector2(cellSize, cellSize));
            }
        }

    }
}
