using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Map map = null;
    [SerializeField] private PlayerCharacter player = null;
    [SerializeField] private Tilemap tilemap = null;
    [SerializeField] private bool useAstar = true;

    public float speed;
    public float reactionTime;

    private AStar astar;
    private Dijkstra dijkstra;
    private int step;
    private int delay;

    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<Map>();
        player = FindObjectOfType<PlayerCharacter>();

        if(useAstar)
        {
            Debug.Log("use A*");
            StartCoroutine(InitAStar());
        }
        else
        {
            Debug.Log("use Dijkstra");
            StartCoroutine(InitDijkstra());
        }
        delay = 0;
    }

    void FixedUpdate()
    {
        if(useAstar)
        {
            if(astar != null && astar.GetPath() != null)
            {
                MoveEnemy(astar.GetPath());
            }
        }
        else
        {
            if (dijkstra != null && dijkstra.GetPath() != null)
            {
                MoveEnemy(dijkstra.GetPath());
            }
        }
    }

    void MoveEnemy(Vector2Int[] path)
    {
        float moveForce = speed * Time.deltaTime;

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

    IEnumerator InitDijkstra()
    {
        Debug.Log("Waiting for map to be built...");
        yield return new WaitWhile(() => !map.IsBuilt());

        while (true)
        {
            dijkstra = new Dijkstra(map.GetGrid(), this, player);
            ClearPath();
            ShowPath(dijkstra.GetPath());
            step = 0;
            delay = 0;
            yield return new WaitForSeconds(reactionTime);
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
            ShowPath(astar.GetPath());
            step = 0;
            delay = 0;
            yield return new WaitForSeconds(reactionTime);
        }
    }

    void ShowPath(Vector2Int[] path)
    {
        Tilemap mapTM = map.GetTilemap();

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
