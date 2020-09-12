using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Map map = null;
    [SerializeField] private PlayerCharacter player = null;
    [SerializeField] private Tilemap tilemap = null;

    public float speed;
    public float reactionTime;

    private AStar astar;
    private int step;
    private int delay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitAStar());
        delay = 0;
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        float moveForce = speed * Time.deltaTime;
        Debug.Log(delay);
        if (astar != null && astar.GetPath() != null)
        {
            Vector2Int[] path = astar.GetPath();

            if(step < path.Length)
            {
                if (delay >= reactionTime * 10)
                {
                    Vector2 targetPosition = new Vector2(path[step].x, path[step].y) + new Vector2(map.cellSize, map.cellSize) * 0.5f;
                    if ((Vector2)transform.position != targetPosition)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveForce);
                    }
                    else
                    {
                        step++;
                        delay = 0;
                    }
                }
                else
                {
                    delay++;
                }
            }
        }
    }

    IEnumerator InitAStar()
    {
        Debug.Log("Waiting for map to be built...");
        yield return new WaitWhile(() => !map.IsBuilt());

        while(true)
        {
            astar = new AStar(map.GetGrid(), this, player);
            ClearPath();
            ShowPath();
            step = 0;
            delay = 0;
            yield return new WaitForSeconds(reactionTime);
        }
    }

    void ShowPath()
    {
        Tilemap mapTM = map.GetTilemap();
        Vector2Int[] path = astar.GetPath();

        for(int i = 0; i < path.Length; i++)
        {
            tilemap.SetTile(new Vector3Int(path[i].x, path[i].y, 0), mapTM.GetTile(new Vector3Int(path[i].x, path[i].y, 0)));
        }
    }

    void ClearPath()
    {
        tilemap.ClearAllTiles();
    }

    public Vector2Int GetPositionOnGrid()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(transform.position.y);

        return new Vector2Int(x, y);
    }
}
