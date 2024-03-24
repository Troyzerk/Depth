using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Net.NetworkInformation;
using static UnityEngine.Rendering.CoreUtils;

/*
 * Master class for initializing scenes
 * 
 * 
 */

public class SceneInitializer : MonoBehaviour
{
    GameObject persistentManagerGameObject;
    public int worldSeed;
    public bool debugFrontend = false;
    public void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        if(MainMenuManager.seed == 0)
        {
            worldSeed = UnityEngine.Random.Range(0, 99999999);
        }
        else
        {
            worldSeed =  MainMenuManager.seed;
        }
        
        UnityEngine.Random.InitState(worldSeed);
        Debug.Log($"World Seed : {worldSeed}");
    }

    public virtual GameObject LoadResources()
    {
        

        if (!GameObject.FindGameObjectWithTag("Frontend"))
        {
            persistentManagerGameObject = CreateAndSetObject("PrimaryPrefabs/PersistentManager", "PersistentManager", true);
        }
        else
        {
            persistentManagerGameObject = GameObject.FindGameObjectWithTag("Frontend");
            DontDestroyOnLoad(persistentManagerGameObject);
            Debug.LogWarning("Could NOT create new PersistantManager because one already exists in the scene. Please remove as this gets created on load.");
        }


        if (!GameObject.FindGameObjectWithTag("GameManager"))
        {
            CreateAndSetObject("PrimaryPrefabs/GameManager", "GameManager", true);
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameManager");
            Debug.LogWarning("Could NOT create new GameManager because one already exists in the scene. Please remove as this gets created on load.");

        }

        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            CreateAndSetObject("PrimaryPrefabs/Player", "Player", true);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player");
            Debug.LogWarning("Could NOT create new player because one already exists in the scene. Please remove as this gets created on load.");
        }

        if (!GameObject.FindGameObjectWithTag("HUD"))
        {
            CreateAndSetObject("PrimaryPrefabs/HUD", "HUD", true);
        }
        else
        {
            GameObject.FindGameObjectWithTag("HUD");
            Debug.LogWarning("Could NOT create new HUD because one already exists in the scene. Please remove as this gets created on load.");
        }

        if (!GameObject.FindGameObjectWithTag("DialogueSystem"))
        {
            CreateAndSetObject("PrimaryPrefabs/DialogueSystem", "DialogueSystem", true);
        }
        else
        {
            GameObject.FindGameObjectWithTag("DialogueSystem");
            Debug.LogWarning("Could NOT create new DialogueSystem because one already exists in the scene. Please remove as this gets created on load.");
        }
        if (debugFrontend)
        {
            print("[FRONTEND] LOADING RESOURCES FINISHED!");
        }

        // Loading Faction Data //
        PersistentManager.factions = Resources.LoadAll("Factions", typeof(Faction)).Cast<Faction>().ToArray();
        //PersistentManager.instance.globalFactions = Resources.LoadAll("Factions", typeof(Faction)).Cast<Faction>().ToArray();

        return persistentManagerGameObject;


    }

    public virtual void PostLoadResources()
    {
        CreateWorld();
        InitGameObjects();
    }

    public GameObject CreateAndSetObject(string path, string name, bool dontDestroyOnLoad)
    {
        GameObject obj = Instantiate(Resources.Load(path) as GameObject);
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(obj);
        }
        obj.name = name;
        return obj;
    }

    [Tooltip("initializes gameobjects like cameracontroller and click col checker")]
    public virtual void InitGameObjects()
    {
        PersistentManager.instance.InitResources();
        PersistentManager.instance.Init();

        QuestManager.instance.Init();

        GameObject.Find("MainCamera").GetComponent<CameraController>().Init();
        GameObject.Find("ClickCol").GetComponent<clickColChecker>().Init();
    }
    public virtual void CreateWorld()
    {
        GameObject obj = CreateAndSetObject("PrimaryPrefabs/WorldGenerator", "WorldGenerator", false);
        obj.GetComponent<WorldGeneratorManager>().CreateWorld();

    }
}