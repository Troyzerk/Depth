using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDragCollide : MonoBehaviour
{
    public BaseGameScript _baseGameScript; 

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseGameScript>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Tile"))
        {
            _baseGameScript.tileHover = col.gameObject;
        }
    }

}
