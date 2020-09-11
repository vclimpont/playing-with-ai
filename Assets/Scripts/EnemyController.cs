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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitAStar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //Debug.Log(player.GetPositionOnGrid());
    }

    IEnumerator InitAStar()
    {
        Debug.Log("Waiting for map to be built...");
        yield return new WaitWhile(() => !map.IsBuilt());

        astar = new AStar(map.GetGrid(), this, player);
    }

    public Vector2 GetPositionOnGrid()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(transform.position.y);

        return new Vector2(x, y);
    }
}
