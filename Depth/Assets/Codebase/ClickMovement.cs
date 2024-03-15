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
    public bool camping;
    //This is for toggling healing 
    public bool healing = true;
    public static bool movementLock = false;

    public Sprite GoblinSprite;
    //Change it from "ArcherGoblin" if you don't want a jump scare ;)
    public Sprite ArcherGoblin;

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

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCamping();
        }

        if (camping && moving)
        {
            StopCamping();
        }
        
        if (moving && (Vector2)transform.position == lastClickedPos)
        {
            moving = false;                                                                                                                                                                                                                                                                                      
        }

        SetTargetPosition();
        SetAgentPosition();
    }

    private void FixedUpdate()
    {
        if (camping && healing)
        {
            StartCoroutine(PartyHeal());
        }
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

    private void StartCamping()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ArcherGoblin;
        camping = true;
        GlobalGameSettings.SetGameSpeed(4);
        Debug.Log("Timescale is" + Time.timeScale);
    }

    public IEnumerator PartyHeal()
    {
        foreach (var character in PlayerData.instance.playerParty.characters)
        {
            if (character.currentHealth < character.health)
            {
                character.currentHealth += 1;
                Debug.Log(character.name + character.currentHealth);
            }
        }
        yield return new WaitForSeconds(12f);
    }

    private void StopCamping()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = GoblinSprite;
        camping = false;
        GlobalGameSettings.SetGameSpeed(1);
        Debug.Log("Timescale is" + Time.timeScale);
    }
}
