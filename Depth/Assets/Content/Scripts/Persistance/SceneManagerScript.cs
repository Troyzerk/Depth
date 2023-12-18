using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance { get; private set; }
    public Scene scene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance = this;
        }
        scene = SceneManager.GetActiveScene();
    }

    public static void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle_Scene");
    }

    public static void LoadBattleResolution()
    {
        SceneManager.LoadSceneAsync("BattleResolution");
    }

    public static void LoadWorldMap()
    {
        SceneManager.LoadScene("LevelTest");
    }

    public static void RecordStoredData()
    {
        GameObject[] towns = GameObject.FindGameObjectsWithTag("Town");
        foreach (GameObject town in towns)
        {
            if (!PersistentManager.instance.storedTowns.Contains(town))
            {
                PersistentManager.instance.storedTowns.Add(town);
            }
        }

        GameObject[] aiGroups = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject npcGroup in aiGroups)
        {
            if (!PersistentManager.instance.storedNPCPartys.Contains(npcGroup))
            {
                PersistentManager.instance.storedNPCPartys.Add(npcGroup);
            }
            
        }
        if (PersistentManager.instance.AIGroups == null && PersistentManager.instance.towns == null)
        {
            PersistentManager.instance.AIGroups = GameObject.Find("AIGroups");
            PersistentManager.instance.towns = GameObject.Find("Towns");
        }
        
        PersistentManager.instance.storedPlayerTransform =  GameObject.FindGameObjectWithTag("Player").transform.position;
        DontDestroyOnLoad(PersistentManager.instance.AIGroups);
        DontDestroyOnLoad(PersistentManager.instance.towns);
        PersistentManager.instance.PrintRecordedData();
    }
}
