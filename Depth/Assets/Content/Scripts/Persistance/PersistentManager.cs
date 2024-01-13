using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager instance { get; private set; }

    public PlayerParty playerParty;
    public Vector3 storedPlayerTransform;
    public AIParty enemyParty;
    public GameObject npcGroup;
    public List<string> globalCharacterNames = new();
    public List<GameObject> storedTowns;
    public List<GameObject> storedNPCPartys;
    public List<Character> activeCharacters;
    public GameObject AIGroups;
    public GameObject towns;
    public bool firstLoad;

    public int startingPlayerPartySize, startingEnemyPartySize;

    [Tooltip("Just used for displaying array. This array should never be called. Use <persistantManager.instance.factions> instead")]
    public Faction[] globalFactions;

    public static List<RaceStats> activeRaces = new();
    public static Faction[] factions;
    

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            firstLoad = true;
            InitGlobalFactions();
            //Adds active Races
            activeRaces.Add(Resources.Load("RaceStats/Goblin_RaceStats") as RaceStats);
            DontDestroyOnLoad(gameObject);

            if (playerParty == null)
            {
                ValidatePlayerParty();
            }
            if (enemyParty == null)
            {
                ValidateNPCParty();
            }
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Destroying new PersistantManager because one already exists");
        }

        if (instance.firstLoad == true)
        {
            instance.firstLoad = false;
            Debug.LogWarning("First Load Init Happening!!!");
            NPCPartySpawner.SpawnNPCGroups(10);
            NPCPartySpawner.SpawnTowns(5);
        }        
    }

    private void InitGlobalFactions()
    {
        factions = Resources.LoadAll("Factions", typeof(Faction)).Cast<Faction>().ToArray();
        globalFactions = Resources.LoadAll("Factions", typeof(Faction)).Cast<Faction>().ToArray();
    }

    public void PrintRecordedData()
    {
        Debug.Log("stored NPC Party amount = " + storedNPCPartys.Count);
        Debug.Log("stored Towns amount = " + storedTowns.Count);
        Debug.Log("stored player party = " + instance.playerParty);
        Debug.Log("stored enemy party = " + instance.enemyParty);
    }

    public void ValidatePlayerParty()
    {
        playerParty = PartyGenerator.GeneratePlayerParty(startingPlayerPartySize);
        if(GameObject.FindGameObjectWithTag("Player")  != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty = playerParty;
        }
        GlobalPlayerData.playerParty = playerParty;
        GlobalHolder.playerPartyReference = playerParty;
    }

    public void ValidateNPCParty()
    {
        enemyParty = PartyGenerator.GenerateNPCParty(startingEnemyPartySize);
    }

    public void ClearAllData()
    {
        Destroy(instance);
    }

    
}
