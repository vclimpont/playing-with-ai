using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSprites : MonoBehaviour
{
    [SerializeField] Sprite openChest = null;

    public void OpenChest()
    {
        GetComponent<SpriteRenderer>().sprite = openChest;
    }
}
