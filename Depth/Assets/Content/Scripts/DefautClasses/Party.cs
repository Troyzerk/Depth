using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Primary party class 
 * all party groups should dirive from this class
 * If we need more vars for combat, trading etc...
 * We should extend this class to cater to them.
 * 
 */

public class Party : ScriptableObject
{
    public Character partyLeader;
    public string partyName;
    public Faction faction;
    public List<Character> characters = new();

    public int gold = 0;

    //start//

    /* These probably all return null atm
     * functionality needs to be added to calculate these. 
     * remove this comment after finishing work on it. 
     */
    public int totalDamage;
    public int totalDefence;
    public float partySpeed;
    public float totalSpeed;
    public int reputation;

    //end//
}


public class PlayerParty : Party
{
    public RaceID startingRace;
    public List<Quest> activeQuests = new();
}


public class AIParty : Party
{
    
    // not implemented yet //
    public GroupComposition comp;
}



/*
 * Group info that are predefined in editor
 * there might be a better way of going about recording this
 * might be better to have these in .csv files and then pull from them.
 */


public static class GroupNames
{
    //public List<string> groupNames;
    public static List<string> AvailableGroupNames = new();

    public static void InitGroupNames()
    {
        AvailableGroupNames.Add("Wizards");
        AvailableGroupNames.Add("Harlets");
        AvailableGroupNames.Add("Bundle Of Sticks");
        AvailableGroupNames.Add("Neverend");
        AvailableGroupNames.Add("Waystrike");
        AvailableGroupNames.Add("Playtime");
        AvailableGroupNames.Add("SlipnSlide");
        AvailableGroupNames.Add("Darkway");
        AvailableGroupNames.Add("Brightway");
        AvailableGroupNames.Add("Neverway");
        AvailableGroupNames.Add("Wizway");
        AvailableGroupNames.Add("Crackles");
        AvailableGroupNames.Add("Leaf Bush");
        AvailableGroupNames.Add("Bandits");
        AvailableGroupNames.Add("Wild Bandits");
        AvailableGroupNames.Add("Sword and Shield");
        AvailableGroupNames.Add("Meat Dish");
        AvailableGroupNames.Add("Heart Charge");
        AvailableGroupNames.Add("Iron Hilt");
        AvailableGroupNames.Add("Still Water");

    }
}



// Does nothing at the moment //
public enum GroupComposition
{
    Mixed,
    Goblins,
    Slimes,
    Adventurers,
    Spiders,
}