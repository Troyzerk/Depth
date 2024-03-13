using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Content/Create new Character")]
public class Character : ScriptableObject
{
    public string characterFullName;
    public string characterFirstName;
    public string characterLastName;
    public string title;

    public RaceID race;
    public SubRaceID subRace;
    public SubRaceStats subRaceStats;

    public Image portrait; 

    public int level;
    public int currentExperience;
    public int maxExperience;

    public float speed;
    public int age;
    public int health;
    public int currentHealth;
    public int mana;
    public int damage;
    public int defence;
    public DamageType damageType;
    public int foodConsumption;
    
    public AutoAttackSkill autoAttackSkill;
    public List <AutoAttackSkill> skills = new();

    public CharacterStatus status = CharacterStatus.Healthy;
    
}

public enum CharacterStatus
{
    Healthy,
    Bleeding, 
    BrokenBones, 
    Tired,
    Injured,
    Dead,
}
