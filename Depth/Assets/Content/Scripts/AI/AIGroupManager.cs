using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//Used to be manager for all groups but i think this can be removed now
//Once the Spawning of AI parties is worked out.

public class AIGroupManager : MonoBehaviour
{
    public GameObject aiGroupPrefab;

    private void Start()
    {
        aiGroupPrefab = Resources.Load("AIGroup") as GameObject;
    }

    public void CheckForAIGroupsSpawn()
    {
        if (GlobalNPCPartyTracker.globalNPCPartys.Count <= 10)
        {
            List<GameObject> towns = GlobalTownTracker.globalTownObjects;
            Transform selectedTownLocation = towns[Random.Range(0, towns.Count)].transform;
            Instantiate(aiGroupPrefab, selectedTownLocation);
        }
    }
}

