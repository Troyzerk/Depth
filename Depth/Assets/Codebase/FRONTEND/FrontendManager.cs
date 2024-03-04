using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.PackageManager;
using UnityEngine;

/*
 * 
 *  This script controls the loading and unloading of Frontend data throughout the game.
 *  Here is where we set singltons like the persistant manager instance and playerdata instance.
 *  
 *  - Troy
 * 
 */

public class FrontendManager : SceneInitializer
{
    public static FrontendManager Instance { get; private set; }
    public static bool isGenerated = false;

    public override void Initialize()
    {
        Debug.Log("[FRONTEND] Init start");
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        base.Initialize();
        if (!isGenerated)
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log($"[FRONTEND] Initialzing Frontend");
            GameObject persistentManager = LoadResources();

            /* 
             * initialize singltons 
             * Do not call anything but [Singlton.instance = x ]
             * this is just for setting instances.
             */

            if (persistentManager.GetComponent<PersistentManager>())
            {
                PersistentManager.instance = persistentManager.GetComponent<PersistentManager>();
                Debug.Log($"[FRONTEND : LOADING] PersistentManager: " + PersistentManager.instance);
            }
            if (persistentManager.GetComponent<PlayerData>())
            {
                PlayerData.instance = persistentManager.GetComponent<PlayerData>();
                Debug.Log($"[FRONTEND : LOADING] Player Data: " + PlayerData.instance);
            }
            if (persistentManager.GetComponent<QuestManager>())
            {
                QuestManager.instance = persistentManager.GetComponent<QuestManager>();
                Debug.Log($"[FRONTEND : LOADING] Quest manager: " + QuestManager.instance);
            }
            if (HUDManager.instance == null)
            {
                HUDManager.instance = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
                Debug.Log($"[FRONTEND : LOADING] World HUD: " + HUDManager.instance);
            }


            PostLoadResources();
            isGenerated = true;
        }
        else
        {
            Debug.Log("[FRONTEND] Finished.");
        }
        
    }

    public override void PostLoadResources()
    {
        

        InitGameObjects();

        base.PostLoadResources();
        Debug.Log($"[FRONTEND] LOADING: Post loading Resource");
    }
}
