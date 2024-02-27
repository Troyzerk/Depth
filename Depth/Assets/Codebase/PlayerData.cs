using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [System.Serializable]
    public class PlayerVarData
    {
        public string playerName;
        public int playerDeaths;
        public int highestLevel;
    }

    public static PlayerData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
        
    }

    private void Start()
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
        PlayerVarData player = new PlayerVarData
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

        PlayerVarData loadedPlayer = JsonUtility.FromJson<PlayerVarData>(json);

        // Now you can access the loaded player data
        string playerName = loadedPlayer.playerName;
        int playerDeaths = loadedPlayer.playerDeaths;
        int highestLevel = loadedPlayer.highestLevel;

        // Use the loaded data in your game
        Debug.Log($"JSON Player Name: {playerName} , Number of Player Deaths : {playerDeaths} ,Highest Level : {highestLevel}");

    }

    public void SaveData(PlayerVarData player)
    {
        string json = JsonUtility.ToJson(player);
        System.IO.File.WriteAllText(Application.dataPath + "/UserData/playerData.json", json);
        Debug.Log("User's data was saved.");
    }
}
