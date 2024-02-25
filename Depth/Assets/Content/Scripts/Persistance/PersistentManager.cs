using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager instance { get; private set; }
    public Character playerCharacter;

    [Tooltip("Starting Race ID for the Player hero character")]
    [SerializeField]
    RaceID heroRaceID;

    [Tooltip("Starting SubRace ID for the Player hero character")]
    [SerializeField]
    SubRaceID heroSubRaceID;

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
    public GameObject landmarks;

    public int startingPlayerPartySize, startingEnemyPartySize;

    [Tooltip("Just used for displaying array. This array should never be called. Use <persistantManager.instance.factions> instead")]
    public Faction[] globalFactions;

    public static List<RaceStats> activeRaces = new();
    public static List<Job> activeJobs = new();
    public static Faction[] factions;
    

    

    private void Awake()
    {


        if (AIGroups == null || !towns || !landmarks)
        {
            AIGroups = GameObject.Find("PersistantManager/AIGroups");
            towns = GameObject.Find("PersistantManager/Towns");
            landmarks = GameObject.Find("PersistantManager/Landmarks");
        }

        if (instance == null)
        {
            instance = this;
            InitResources(); // Resources should always be loaded before first load.
            DontDestroyOnLoad(gameObject);
            FirstLoad();
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Destroying new PersistantManager because one already exists");
        }

    }

    public void InitResources()
    {
        //Loading the Goblin Race
        activeRaces.Add(Resources.Load("RaceStats/Goblin_RaceStats") as RaceStats);

        //Get all jobs
        Job[] tempArray = Resources.LoadAll<Job>("Job");
        foreach (Job job in tempArray)
        {
            activeJobs.Add(job);
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


    // Validation 

    //First Load will probably have to be reworked when we go to save this data and load it. 
    public void FirstLoad()
    {
        InitGlobalFactions();
        ValidatePlayerParty();
        ValidatePlayerCharacter();
        ValidateNPCParty();

        //Spawning of content should be moved out of persistant manager into a generator 
        NPCPartySpawner.SpawnNPCGroups(10);
        NPCPartySpawner.SpawnTowns(5);
        NPCPartySpawner.SpawnLandmark(20);


    }

    public void ValidatePlayerCharacter()
    {
        playerCharacter = CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty.partyLeader = playerCharacter;
    }
    public void ValidatePlayerParty()
    {
        playerParty = PartyGenerator.GeneratePlayerParty(startingPlayerPartySize);
        GlobalPlayerData.playerParty = playerParty;
        GlobalHolder.playerPartyReference = playerParty;

        // This is weird, should probably get reworked because there isnt always a PlayerPartyManager on the player
        if (GameObject.FindGameObjectWithTag("Player")  != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty = playerParty;
        }
        
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
