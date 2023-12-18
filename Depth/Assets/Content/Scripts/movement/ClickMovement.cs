using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMovement : MonoBehaviour
{
    PlayerPartyManager playerPartyManager;
    public Vector2 lastClickedPos;
    public GameObject clickCol;
    public bool moving;
    public static bool movementLock = false;

    private void Start()
    {
        playerPartyManager = GameObject.Find("Player").GetComponent<PlayerPartyManager>();
        clickCol = GameObject.Find("ClickCol");
    }

    public void UpdatedSelectedLocation()
    {
        lastClickedPos = clickCol.gameObject.GetComponent<clickColChecker>().otherColLocation;
        clickCol.gameObject.transform.position = lastClickedPos;
    }

    public void MoveLock()
    {
        movementLock = !movementLock;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false && movementLock == false)
        {
            lastClickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickCol.transform.position = lastClickedPos;
            moving = true;
            clickCol.gameObject.GetComponent<clickColChecker>().check = false;
        }

        if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = playerPartyManager.playerParty.partySpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        } else
        {
            moving = false;                                                                                                                                                                                                                                                                                      
        }
    }

}
