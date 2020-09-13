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
    private int collectedCoins; 

    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<Map>();

        canMove = true;
        targetPosition = transform.position;
        delay = 0;
        collectedCoins = 0;
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

    public Vector2Int GetPositionOnGrid()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int y = Mathf.FloorToInt(transform.position.y);

        return new Vector2Int(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
          if (collision.CompareTag("Enemy"))
        {
            FindObjectOfType<GameManager>().EndGame();
        }
          else if (collision.CompareTag("Coin"))
        {
            collectedCoins++;
            Destroy(collision.gameObject);
        }
          else if (collision.CompareTag("Chest") && collectedCoins == map.numberOfCoins)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
