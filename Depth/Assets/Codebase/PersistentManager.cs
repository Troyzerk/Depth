using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager instance { get; private set; }
    public Character playerCharacter;

    public TextAsset skillTable;


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
    }

    public void ValidatePlayerCharacter()
    {
        playerCharacter = CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty.partyLeader = playerCharacter;
    }
    public void ValidatePlayerParty()
    {
        PlayerData.instance.playerParty = PartyGenerator.GeneratePlayerParty(startingPlayerPartySize);
        GlobalPlayerData.playerParty = PlayerData.instance.playerParty;
        GlobalHolder.playerPartyReference = PlayerData.instance.playerParty;

        // This is weird, should probably get reworked because there isnt always a PlayerPartyManager on the player
        if (GameObject.FindGameObjectWithTag("Player")  != null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty = PlayerData.instance.playerParty;
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
