using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEditor.SearchService;

public class ClickMovement : MonoBehaviour
{
    PlayerPartyManager playerPartyManager;
    public Vector2 lastClickedPos;
    public GameObject clickCol;
    public bool moving;
    public static bool movementLock = false;

    //NavMesh Variables
    private Vector3 target;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        clickCol = GameObject.Find("ClickCol");
        
    }

    public void Reload()
    {
        clickCol = GameObject.Find("ClickCol");
        print(clickCol);
        target = agent.transform.position;
        lastClickedPos = agent.transform.position;
        clickCol.transform.position = agent.transform.position;
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

        /*if (moving && (Vector2)transform.position != lastClickedPos)
        {
            float step = playerPartyManager.playerParty.partySpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickedPos, step);
        } else
        {
            moving = false;                                                                                                                                                                                                                                                                                      
        }*/

        SetTargetPosition();
        SetAgentPosition();
    }

    private void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
}
