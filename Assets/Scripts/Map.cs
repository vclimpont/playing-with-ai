﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private PlayerCharacter player = null;
    [SerializeField] private EnemyController enemy = null;

    public int width;
    public int height;
    public float cellSize;
    public int enemyMinDistFromPlayer;

    [SerializeField] private Tilemap tilemap = null;
    [SerializeField] private Tile t_ground = null;
    [SerializeField] private Tile t_grass = null;
    [SerializeField] private Tile t_grav = null;
    [SerializeField] private Tile t_rock = null;

    public float obstacle_freq;

    private Grid grid = new Grid(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(width, height, cellSize);
        mainCamera.transform.position = new Vector3(width * cellSize / 2, height * cellSize / 2, -10f);
        mainCamera.orthographicSize = Mathf.Max(width * cellSize / 2, height * cellSize / 2);

        BuildMap();
        SetPlayerPositionOnStart();
        SetEnemyPositionOnStart();
    }

    void BuildMap()
    {
        for (int i = 0; i < grid.GetGridArray().GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
            {
                float r = Random.Range(0f, 1f);

                if (r < obstacle_freq)
                {
                    grid.SetValue(i, j, 1);
                    tilemap.SetTile(new Vector3Int(i, j, 0), t_rock);
                }
                else if(r < 1f - ((1f - obstacle_freq) / 2))
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), t_ground);
                }
                else if(r < 1f - ((1f - obstacle_freq) / 4))
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), t_grass);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(i, j, 0), t_grav);
                }
            }
        }
    }

    void SetPlayerPositionOnStart()
    {
        int x, y;

        do
        {
            x = Random.Range(0, GetGrid().GetGridArray().GetLength(0));
            y = Random.Range(0, GetGrid().GetGridArray().GetLength(1));
        } while (GetGrid().GetValue(x, y) == 1);

        Vector2 positionOnStart = new Vector2(x,y) + new Vector2(cellSize, cellSize) * 0.5f;
        player.transform.position = positionOnStart;
    }

    void SetEnemyPositionOnStart()
    {
        int x, y;
        float distFromPlayer;

        do
        {
            x = Random.Range(0, GetGrid().GetGridArray().GetLength(0));
            y = Random.Range(0, GetGrid().GetGridArray().GetLength(1));
            distFromPlayer = Mathf.Abs(player.transform.position.x - x) + Mathf.Abs(player.transform.position.y - y);
            Debug.Log(distFromPlayer);

        } while (GetGrid().GetValue(x, y) == 1 || distFromPlayer < enemyMinDistFromPlayer);

        Vector2 positionOnStart = new Vector2(x, y) + new Vector2(cellSize, cellSize) * 0.5f;
        enemy.transform.position = positionOnStart;
    }

    public Grid GetGrid()
    {
        return grid;
    }

    //void OnDrawGizmos()
    //{
    //    for (int i = 0; i < grid.GetGridArray().GetLength(0); i++)
    //    {
    //        for (int j = 0; j < grid.GetGridArray().GetLength(1); j++)
    //        {
    //            if(grid.GetValue(i,j) == 1)
    //            {
    //                Gizmos.color = Color.red;
    //            }
    //            Gizmos.DrawSphere(grid.GetWorldPosition(i, j), cellSize / 5f);
    //        }
    //    }

    //}
}