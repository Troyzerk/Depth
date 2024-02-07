using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Leaving Tile");
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
