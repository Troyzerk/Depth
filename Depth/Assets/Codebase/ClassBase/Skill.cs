using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Passive,
    Active,
    AutoAttack,
    AOE,
}

/*
 * Please keep base class clean. No functionality should be contained here. 
 * We should only be putting very base functionality. 
 * If you want to create different functionality use overrides.
 * 
 * This is the main system for our game so we have to be very careful with 
 * preventing it from getting messy. 
 */

public class Skill : ScriptableObject
{
    public string skillName;
    public DamageType damageType;
    public SkillType skillType;

    //needs renaming
    public int damageDelt;

    [Tooltip("Defines the variety in damage delt : if set to 5 damage will randomise 5 +/-. If Damage is 10 then damage ranges from 5-15")]
    public int damageVariety;

    public int range;
    public float cooldown;

    public void Init()
    {
        
    }
    public virtual void Cast()
    {
        //Ability Cast
        //Example : Spawn Object, apply damage
    }
}


public class AutoAttack : Skill
{

}

public class MeleeAutoAttack : AutoAttack
{

}

public class RangedAutoAttack : AutoAttack
{

}

/*
 * Damage Abilities 
 * The first class is the BaseDamage ability class
 * 
 */


[CreateAssetMenu(fileName = "New Damage Skill", menuName = "Content/Damage/DamageSkill")]
public class DamageSkill : Skill
{
    public override void Cast()
    {
        base.Cast();
    }
}

[CreateAssetMenu(fileName = "New Damage Skill", menuName = "Content/Damage/TargetDamageSkill")]
public class TargetDamageAbility : DamageSkill
{
    public override void Cast()
    {
        base.Cast();
    }
}
[CreateAssetMenu(fileName = "New Damage Skill", menuName = "Content/Damage/AOEDamageSkill")]
public class AOEDamageSkill : DamageSkill
{
    public string Hello;
    public override void Cast()
    {
        base.Cast();
    }
}

/*
 * Healing Abilities 
 * The first class is the Base Healing ability class
 * 
 */
public class HealingSkill : Skill
{
    public override void Cast()
    {
        base.Cast();

    }
}
public class TargetHealingSkill : HealingSkill
{
    public override void Cast()
    {
        base.Cast();
    }
}
public class AOEHealingSkill : HealingSkill
{
    public override void Cast()
    {
        base.Cast();
    }
}

