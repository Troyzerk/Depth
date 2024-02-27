using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Races", menuName = "Global/Races")]
public class GlobalRaces : ScriptableObject
{
    public RaceStats raceStats;

}


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




