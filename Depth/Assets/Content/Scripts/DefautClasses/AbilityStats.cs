using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    Passive, 
    Active,
    AutoAttack,
    AOE,
}

[CreateAssetMenu(fileName = "New Ability Stats", menuName = "Content/Ability/AbilityStats")]
public class AbilityStats : ScriptableObject
{
    public string skillName;
    public DamageType damageType;
    public AbilityType skillType;

    public int damageDelt;

    [Tooltip("Defines the variety in damage delt : if set to 5 damage will randomise 5 +/-. If Damage is 10 then damage ranges from 5-15")]
    public int damageVariety;

    public int range;
    public float cooldown;
}

