using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private Map map = null;

    public float speed;

    private Vector2 moveInput;
    private bool canMove;
    private Vector2 targetPosition;
    private int delay;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        targetPosition = transform.position;
        delay = 0;
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
        float moveForce = speed * Time.deltaTime;

        if(canMove)
        {
            targetPosition = (Vector2)transform.position + new Vector2(moveInput.x, moveInput.y);
        }

        if (CanMoveHere() && (Vector2)transform.position != targetPosition)
        {
            canMove = false;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveForce);
        }
        else
        {
            if(delay >= 5)
            {
                canMove = true;
                delay = 0;
            }
            else
            {
                delay++;
            }
        }
    }

    bool CanMoveHere()
    {
        int x = Mathf.FloorToInt(targetPosition.x);
        int y = Mathf.FloorToInt(targetPosition.y);

        if (x < 0 || x >= map.GetGrid().GetGridArray().GetLength(0) || y < 0 || y >= map.GetGrid().GetGridArray().GetLength(1))
        {
            return false;
        }
        else if (map.GetGrid().GetGridArray()[x, y] == 1)
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
