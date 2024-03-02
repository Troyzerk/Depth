using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance { get; set; }
    public event EventHandler OnPlayerDataValidated;

    //Quest
    public List<Quest> quests;
    public Quest activeQuest;

    // Race
    [Tooltip("Starting Race ID for the Player hero character")]
    [SerializeField]
    RaceID heroRaceID;

    [Tooltip("Starting SubRace ID for the Player hero character")]
    [SerializeField]
    SubRaceID heroSubRaceID;

    //Vars
    public Character playerCharacter;
    public PlayerParty playerParty;
    public GameObject playerPartyObject;
    public Vector3 partyTransform;



    [System.Serializable]
    public class PlayerSaveData
    {
        public string playerName;
        public int playerDeaths;
        public int highestLevel;
    }


    public void ValidatePlayerCharacter()
    {
        playerCharacter = CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty.partyLeader = playerCharacter;
        PersistentManager.instance.playerCharacter = playerCharacter;
    }

    // LOADING JSON //
    public void CheckForLoadedData()
    {
        if (System.IO.File.Exists(Application.dataPath + "/UserData/playerData.json"))
        {
            LoadData();
        }
        else
        {
            CreateNewData("User");
        }
    }
    public void CreateNewData(string username)
    {
        PlayerSaveData player = new PlayerSaveData
        {
            playerName = username,
            playerDeaths = 0,
            highestLevel = 0
        };
        SaveData( player );
    }
    public void LoadData()
    {
        string json = System.IO.File.ReadAllText(Application.dataPath + "/UserData/playerData.json");

        PlayerSaveData loadedPlayer = JsonUtility.FromJson<PlayerSaveData>(json);

        // Now you can access the loaded player data
        string playerName = loadedPlayer.playerName;
        int playerDeaths = loadedPlayer.playerDeaths;
        int highestLevel = loadedPlayer.highestLevel;

        // Use the loaded data in your game
        Debug.Log($"JSON Player Name: {playerName} , Number of Player Deaths : {playerDeaths} ,Highest Level : {highestLevel}");

    }
    public void SaveData(PlayerSaveData player)
    {
        string json = JsonUtility.ToJson(player);
        System.IO.File.WriteAllText(Application.dataPath + "/UserData/playerData.json", json);
        Debug.Log("User's data was saved.");
    }
}
