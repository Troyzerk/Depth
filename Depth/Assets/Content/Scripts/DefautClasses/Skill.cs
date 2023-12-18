using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Content/New Skill")]
public class Skill : ScriptableObject
{
    public int range;
    public string skillName;
    public float cooldown;
    public int damageDelt;
    public DamageType damageType;
    public SkillType skillType;

    public void Cast(GameObject target, int characterDamage)
    {
        // Start cooldown
    }

}

public enum SkillType
{
    Passive, 
    Active,
    AutoAttack,
}
