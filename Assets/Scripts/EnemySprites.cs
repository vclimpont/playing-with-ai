using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySprites : MonoBehaviour
{
    [SerializeField] Sprite[] sprites = null;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        int r = Mathf.FloorToInt(Random.Range(0f, sprites.Length));
        sr.sprite = sprites[r];
    }
}
