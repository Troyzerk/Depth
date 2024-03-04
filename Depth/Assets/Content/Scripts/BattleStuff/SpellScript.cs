using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    public GameObject enemyTarget;
    public GameObject friendlyTarget;

    public void DealDamage()
    {
        print(enemyTarget);
        if (enemyTarget != null)
        {
            int damage = 1;
        
            int health = enemyTarget.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
            health -= damage;
            enemyTarget.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        }
    }

    public void DealHeal( int damage)
    {
        int health = friendlyTarget.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
        health -= damage;
        friendlyTarget.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
    }
}
public class SkillOther : ScriptableObject
{
    public DamageType damageType;
    public SkillType skillType;

    //needs renaming
    public int damage;

    [Tooltip("Defines the variety in damage delt : if set to 5 damage will randomise 5 +/-. If Damage is 10 then damage ranges from 5-15")]
    public int damageVariety;

    public int range;
    public float cooldown;

    public GameObject skillPrefab;
    public Sprite buttonSprite;


    public void Init()
    {

    }
    public virtual void Cast()
    {
        //Ability Cast
        //Example : Spawn Object, apply damage
        skillPrefab.gameObject.GetComponent<spriteDeath>().coolDown = cooldown;


    }
    public virtual void postCast()
    {

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

