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

    public override void Initialize()
    {
        base.Initialize();
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
        
        
        PostLoadResources();
    }

    public override void PostLoadResources()
    {
        PersistentManager.instance.InitResources();
        PersistentManager.instance.Init();

        QuestManager.instance.Init();
        //PersistentManager.instance.ValidateNPCParty();

        base.PostLoadResources();
        Debug.Log($"[FRONTEND] LOADING: Post loading Resource");
    }
}
