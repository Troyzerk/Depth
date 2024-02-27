using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalPlayerData
{
    public static GameObject player;
    public static PlayerParty playerParty;
    public static Town selectedTown;
}

public static class GlobalTownTracker
{
    public static List<GameObject> globalTownObjects= new();
    public static List<string> globalTownNames = new();
    public static List<Town> globalTownStats = new();
}

public static class GlobalNPCPartyTracker
{
    public static List<GameObject> globalNPCPartyObjects = new();
    public static List<string> globalNPCGroupNames = new();
    public static List<AIParty> globalNPCPartys = new();
    public static void UpdateAIGroupTracker()
    {
        globalNPCPartys.Clear();
        var gameObjects = GameObject.FindGameObjectsWithTag("AI");
        foreach (var gameObject in gameObjects)
        {
            globalNPCPartys.Add(gameObject.GetComponent<AIBehaviour>().aiParty);
        }
        foreach (var party in globalNPCPartys)
        {
            globalNPCGroupNames.Add(party.partyName);
        }
    }
}

public static class GlobalCharacterTracker
{

}
