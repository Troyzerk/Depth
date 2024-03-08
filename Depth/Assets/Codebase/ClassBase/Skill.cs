using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Passive,
    Active,
    AutoAttack
}
public enum SkillIntent
{
    Damage,
    Heal
}

/*
 * Please keep base class clean. No functionality should be contained here. 
 * We should only be putting very base functionality. 
 * If you want to create different functionality use overrides.
 * 
 * This is the main system for our game so we have to be very careful with 
 * preventing it from getting messy. 
 */
[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/normalSkill")]
public class Skill : ScriptableObject
{
    public string skillName;

    public DamageType damageType;
    public SkillType skillType;
    public SkillIntent skillIntent;

    //needs renaming
    public int damage;

    [Tooltip("Defines the variety in damage delt : if set to 5 damage will randomise 5 +/-. If Damage is 10 then damage ranges from 5-15")]
    public int damageVariety;

    public int range;
    public float cooldown;

    public Sprite buttonSprite;

    public virtual void Awake()
    {
        Debug.Log("Hello there");
    }

    public virtual void Cast()
    {
        Debug.Log("Cast");
    }
    public virtual IEnumerator OverTime(GameObject self, GameObject target)
    {
        yield return null;

    }
}
