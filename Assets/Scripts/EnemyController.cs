using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Map map = null;
    [SerializeField] private PlayerCharacter player = null;

    public float speed;
    public float reactionTime;

    private AStar astar;
    private int step;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitAStar());
        step = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        float moveForce = speed * Time.deltaTime;

        if (astar != null && astar.GetPath() != null)
        {
            Vector2Int[] path = astar.GetPath();

            if(step < path.Length)
            {
                Vector2 targetPosition = new Vector2(path[step].x, path[step].y) + new Vector2(map.cellSize, map.cellSize) * 0.5f;
                if ((Vector2)transform.position != targetPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveForce);
                }
                else
                {
                    step++;
                }
            }
        }
    }

    IEnumerator InitAStar()
    {
        Debug.Log("Waiting for map to be built...");
        yield return new WaitWhile(() => !map.IsBuilt());

        astar = new AStar(map.GetGrid(), this, player);
    }

    public Vector2Int GetPositionOnGrid()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(transform.position.y);

        return new Vector2Int(x, y);
    }
}
