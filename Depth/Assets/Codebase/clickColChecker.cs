using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickColChecker : MonoBehaviour
{
    public bool check = false;
    public Vector2 otherColLocation;
    public ClickMovement clickMove;
    private void Start()
    {
        clickMove = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Town"))
        {
            check = true;
            otherColLocation = other.gameObject.transform.position;
            clickMove.UpdatedSelectedLocation();
        }
        else
        {
            check = false;
            otherColLocation = other.gameObject.transform.position;
        }
    }
}
