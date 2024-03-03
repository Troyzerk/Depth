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
    public int reputation = 0;

    //start//

    /* These probably all return null atm
     * functionality needs to be added to calculate these. 
     * remove this comment after finishing work on it. 
     */

    public int totalDamage;
    public int totalDefence;
    public float partySpeed;
    public float totalSpeed;

    
    

    //end//
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