using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *  Having Enums like this is okay, Its more of a personal preference if you wnat to hold them together or not. 
 *  In this project we will group enums by kind. In this instance all enums here are related to vars in the 
 *  race/subrace classes. 
 * 
 *  We might want to think about removing the faction and damage stuff to its own .cs file though. 
 *  
 *  - Troy Kinane 01/03/24
 * 
 */


public enum RaceID
{
    Goblin,
    Slime,
    Spider,
    Zombie,
}

public enum SubRaceID
{
    Changling,
    Gremlin,
    Goblinoid,
    MatureGoblin,
    Ogre,
    MatureOgre,
    ElderOgre,
    StoneOgre,
    UndeadOgre,
}

public enum SlimeEvos
{
    Slime,
}

public enum Relationship
{
    friendly, 
    Neutral, 
    Hostile,
}

public enum FactionID
{
    KingdomOfKong,
    Allies,
    WildlandsFoke,
}

public enum DamageType
{
    Physical,
    Water,
    Fire,
    Earth,
    Electric,
    Poision,
    Arcane,
    Nature,
    Rot,
    Holy,
    Demonic,
    Forgotten,
}




