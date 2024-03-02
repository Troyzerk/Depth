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
    public void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {

    }

    public virtual GameObject LoadResources()
    {
        

        if (!GameObject.FindGameObjectWithTag("Frontend"))
        {
            persistentManagerGameObject = CreateAndSetObject("PrimaryPrefabs/PersistantManager", "PersistentManager", true);
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


        


        // Loading Faction Data //
        PersistentManager.factions = Resources.LoadAll("Factions", typeof(Faction)).Cast<Faction>().ToArray();
        //PersistentManager.instance.globalFactions = Resources.LoadAll("Factions", typeof(Faction)).Cast<Faction>().ToArray();

        return persistentManagerGameObject;


    }

    public virtual void PostLoadResources()
    {
        CreateWorld();
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

    public virtual void CreateWorld()
    {
        GameObject obj = CreateAndSetObject("PrimaryPrefabs/WorldGenerator", "WorldGenerator", false);
        obj.GetComponent<WorldGeneratorManager>().CreateWorld();

    }
}