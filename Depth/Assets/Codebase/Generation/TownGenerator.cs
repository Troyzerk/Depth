using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public static class TownGenerator
{
    public static Town GenerateTownStats(TownStats townStats, GameObject townGameObject)
    {
        TownMenuManager townMenuManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TownMenuManager>();
        Town town = ScriptableObject.CreateInstance<Town>();

        //Sets random stats for the new town

        //Figuring out name//

        town.owningRace = RaceID.Goblin;
        town.townName = GenerateTownName(town, townStats);
        town.owningRaceDamageType = townStats.owningRaceDamageTypes[Random.Range(0, townStats.owningRaceDamageTypes.Count)];
        town.owningRace = RaceID.Goblin;
        town.portrait = townStats.portraits[Random.Range(0, townStats.portraits.Count)];
        town.age = Random.Range(townStats.minStartingAge, townStats.maxStartingAge);
        town.defence = Random.Range(townStats.minStartingDefence, townStats.maxStartingDefence);
        town.buildings = new();
        town.buildings.Add(BuildingType.Church);
        town.buildings.Add(BuildingType.Inn);



        //Sets name & description
        town.name = town.townName;
        townGameObject.name = town.townName;

        town.description = "Welcome to " + town.townName + ". Its home to many a " + town.owningRace.ToString() + ". Stop by the church to heal your party.";

        //Sets global tracker to include this town.
        GlobalTownTracker.globalTownStats.Add(town);
        GlobalTownTracker.globalTownNames.Add(town.townName);
        GlobalTownTracker.globalTownObjects.Add(townGameObject);

        // setting dialogue for town //
        /*
         * We will have to somehow generate this dialogue. 
         * Im thinking that we can just have preset dialogue options for towns, 
         * then pull from those when needed. 
         */
        town.dialogue = new string[2];
        town.dialogue[0] = "Our friend has finally arrived at " + town.name + ".";
        town.dialogue[1] = "Where they will meet a new friend of sorts... actually I guess it depends on what your definition of friend is.";

        town.priestDialogue = new string[2];
        town.priestDialogue[0] = "Hello my friend. Welcome to the church of " + town.name + ".";
        town.priestDialogue[1] = "I will bless you and your friends with healing.";

        return town;
    }
    public static string GenerateTownName(Town newTown, TownStats townStats)
    {
        TownMenuManager townMenuManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TownMenuManager>();
        newTown.townName = townStats.townNames[Random.Range(0, townStats.townNames.Count)];
        if (GlobalTownTracker.globalTownNames.Contains(newTown.name))
        {
            GenerateTownName(newTown, townStats);
            //print("Town name already found");
            return null;
        }
        else
        {
            //print(newTown.townName + " was created.");
            return newTown.townName;
        }
    }
}
