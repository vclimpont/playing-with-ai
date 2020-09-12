using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Map map = null;

    public float speed;

    private Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if(CanMoveHere())
        {
            Vector2 moveForce = new Vector2(moveInput.x, moveInput.y) * speed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x + moveForce.x, transform.position.y + moveForce.y);
        }
    }

    bool CanMoveHere()
    {
        Vector2Int pos = GetPositionOnGrid();
        int x = pos.x + Mathf.FloorToInt(moveInput.x);
        int y = pos.y + Mathf.FloorToInt(moveInput.y);
        Debug.Log(GetPositionOnGrid());
        Debug.Log(x + " " + y);

        if (x < 0 || x >= map.GetGrid().GetGridArray().GetLength(0) || y < 0 || y >= map.GetGrid().GetGridArray().GetLength(1))
        {
            return false;
        }
        else if(map.GetGrid().GetGridArray()[x, y] == 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void SetPositionOnStart()
    {
        int x, y;

        do
        {
            x = Random.Range(0, map.GetGrid().GetGridArray().GetLength(0));
            y = Random.Range(0, map.GetGrid().GetGridArray().GetLength(1));
        } while (map.GetGrid().GetValue(x, y) == 1);

        Vector2 positionOnStart = new Vector2(x * map.cellSize, y * map.cellSize);
        transform.position = positionOnStart;
    }

    public Vector2Int GetPositionOnGrid()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(transform.position.y);

        return new Vector2Int(x, y);
    }
}
