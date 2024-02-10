using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public GameObject bannerText;

    //REWORK THIS AS THIS IS THE OLD AIGROUP CLASS.
    //we now use AIParty:Party Class in the Party.cs script
    public AIParty aiParty;
    public AIGroupManager manager;

    //movement vars
    public Vector2 moveLocation;
    public bool moving;
    public bool isChasing = false;
    public bool beingChased = false;
    public GameObject target;
    public GameObject hunter;
    public float distanceToClosestTarget;
    public float huntingRange = 6f;
    public Faction CurrentFaction;

    //hunting ground vars
    public Vector2 huntingGroundCenter;
    public float huntingGroundSize;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AIGroupManager>();

        aiParty = PartyGenerator.GenerateNPCParty(Random.Range(1, 5));
        gameObject.name = aiParty.name;
        GlobalNPCPartyTracker.globalNPCPartyObjects.Add(gameObject);
        GlobalNPCPartyTracker.globalNPCPartys.Add(aiParty);
        GlobalNPCPartyTracker.globalNPCGroupNames.Add(aiParty.partyName);

        TextMesh bannerPartyCountText = bannerText.GetComponent<TextMesh>();
        bannerPartyCountText.text = aiParty.characters.Count.ToString();

        CurrentFaction = aiParty.faction;

        huntingGroundCenter = new Vector2(Random.Range(-7, 7), Random.Range(-7, 7));
        huntingGroundSize = Random.Range(1, 3);

        UpdateMoveLocation();
    }

    private void FixedUpdate()
    {
        //chasing when not chased by significantly superior force
        if (beingChased == false)
        {
            Chase();
        }

        //movement
        Movement();

        //Find target if no current target and not being chased by significantly superior force
        if (target == null && beingChased == false)
        {
            FindTarget();
        }
    }

    private void Chase()
    {
        //StartBattle();
        if (moving && isChasing && (Vector2)transform.position != moveLocation && target != null)
        {
            moveLocation = target.transform.position;
            Debug.DrawLine(this.transform.position, target.transform.position, Color.red);
        }
    }

    private void Movement()
    {
        if (moving == true && (Vector2)transform.position != moveLocation)
        {
            float step = aiParty.partySpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, moveLocation, step);
        }
        else if (moving == true && (Vector2)transform.position == moveLocation)
        {
            StartCoroutine(WaitAtDestination());
        }
    }

    /*Possible performance concerns
     * 
     * Limitations are: tracks too many ai objects constantly regardless of distance, limiting max number of parties
     * 
     * Possible Solutions: Localized trigger boxes that track distances instead (Limited the number of calls based on faction)
     * 
    */
    private void FindTarget()
    {
        GameObject playerPartyObject = GameObject.FindGameObjectWithTag("Player");

        distanceToClosestTarget = huntingRange;
        //PersistentManager.instance.storedNPCPartys
        //This is where NPC partys are stored while loading between levels

        for (int i = 0; i < GlobalNPCPartyTracker.globalNPCPartyObjects.Count; i++)
        {
            GameObject newTarget = GlobalNPCPartyTracker.globalNPCPartyObjects[i];
            if (aiParty.faction.neutralFactions == null && aiParty.faction.enemyFactions == null && aiParty.faction.friendlyFactions == null)
            {
                Debug.LogError("Faction scriptable objects have broken relationships returning all as null");
            }

            if (newTarget.gameObject != null && aiParty.faction.enemyFactions.Contains(newTarget.gameObject.GetComponent<AIBehaviour>().aiParty.faction.factionName) && this.aiParty.totalDamage >= 1f * (newTarget.gameObject.GetComponent<AIBehaviour>().aiParty.totalDamage))
            {
                float distanceToTarget = Vector2.Distance(this.transform.position, newTarget.transform.position);

                if (distanceToTarget < distanceToClosestTarget && GlobalNPCPartyTracker.globalNPCPartyObjects[i].name != this.name)
                {
                    target = newTarget;
                    distanceToClosestTarget = Vector2.Distance(this.transform.position, GlobalNPCPartyTracker.globalNPCPartyObjects[i].transform.position);
                    if (distanceToClosestTarget <= Vector2.Distance(playerPartyObject.transform.position, this.transform.position))
                    // && this.aiParty.totalDamage >= 0.4f * (PersistentManager.instance.playerParty.totalDamage)
                    {
                        target = playerPartyObject;
                        Debug.Log(target);
                    }
                    else if (this.aiParty.totalDamage >= 1.2f * (target.gameObject.GetComponent<AIBehaviour>().aiParty.totalDamage))
                    {
                        target.GetComponent<AIBehaviour>().Nigeru(this.gameObject);
                        //Debug.Log("Hunting " + target);
                    }
                }
            }
        }

        isChasing = true;
    }

    //Flee from hunter (called by the hunter instead of constantly checking)
    public void Nigeru(GameObject newHunter)
    {
        hunter = newHunter;
        this.beingChased = true;
        this.isChasing = false;
        Debug.Log(this.gameObject + " is hunted by " + hunter);

        moveLocation = new Vector2((this.transform.position.x - hunter.transform.position.x) * -1, (this.transform.position.y - hunter.transform.position.y) * -1);
        /*
        if (Vector2.Distance(transform.position, hunter.transform.position) <= huntingGroundSize)
        {
            moveLocation = new Vector2(hunter.transform.position.x * -1, hunter.transform.position.y * -1);
        }
        else
        {
            UpdateMoveLocation();
        }
        */
    }

    private void CheckTail()
    {
        Debug.Log("CheckingTail");
        
        if (hunter.gameObject.GetComponent<AIBehaviour>().target == this.gameObject)
        {
            moveLocation = new Vector2((this.transform.position.x - hunter.transform.position.x) * -1, (this.transform.position.y - hunter.transform.position.y) * -1);
        }
        else
        {
            hunter = null;
            this.beingChased = false;
            Debug.Log("No longer hunted");
            FindTarget();
        }
    }

    private void UpdateMoveLocation()
    {
        moveLocation = new Vector2(Random.Range(huntingGroundCenter.x - huntingGroundSize, huntingGroundCenter.x + huntingGroundSize), Random.Range(huntingGroundCenter.y - huntingGroundSize, huntingGroundCenter.y + huntingGroundSize));
        moving = true;
    }

    //Waiting time is reduced if being actively chased by significantly superior force
    IEnumerator WaitAtDestination()
    {
        moving = false;
        if (beingChased == true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            CheckTail();
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 5f));
            UpdateMoveLocation();
        }
    }

    private void StartBattle()
    {
        if (moving && isChasing && (Vector2)transform.position == moveLocation)
        {
            bool attackingPartyWon = manager.GetComponent<GameState>().ResolveBattle(aiParty.characters, target.GetComponent<AIParty>().characters);
            if (attackingPartyWon)
            {
                Destroy(target.gameObject);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AIGroupManager>().CheckForAIGroupsSpawn();
                target = null;
                isChasing = false;
                StartCoroutine(WaitAtDestination());
            }
            else if (!attackingPartyWon && aiParty.characters.Count <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                print("Fight not fully Resolved");
            }
        }
    }

}
