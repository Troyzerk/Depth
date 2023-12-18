using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RaceStats", menuName = "Global/Races/RaceStats")]
public class RaceStats : ScriptableObject
{
    public RaceID raceID;
    public SubRaceID subRaceID;
    public SubRaceStats subRaceStats;

    public List<SubRaceStats> possibleSubRaces = new();
    public List<string> raceFirstNames = new();
    public List<string> raceLastNames = new();
    public List<Sprite> portraits = new();
    public List<DamageType> startingDamageType = new();
    public List<Skill> startingAutoAttackSkill = new();


    public int maxStartingHealth;
    public int minStartingHealth;

    public int maxStartingAge;
    public int minStartingAge;

    public int maxStartingMana;
    public int minStartingMana;

    public int maxStartingDamage;
    public int minStartingDamage;

    public int maxStartingDefence;
    public int minStartingDefence;

    public float maxStartingSpeed;
    public float minStartingSpeed;

}




