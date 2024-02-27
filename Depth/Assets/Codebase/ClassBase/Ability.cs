using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public AbilityStats abilityStats;

    public void Init()
    {

    }
    public virtual void Cast()
    {
        //Ability Cast
        //Example : Spawn Object, apply damage
    }
}

/*
 * Damage Abilities 
 * The first class is the BaseDamage ability class
 * 
 */
public class DamageAbility : Ability
{
    public override void Cast()
    {
        base.Cast();
    }
}
public class TargetDamageAbility : DamageAbility
{
    public override void Cast()
    {
        base.Cast();
    }
}
public class AOEDamageAbility : DamageAbility
{
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
public class HealingAbility : Ability
{
    public override void Cast()
    {
        base.Cast();

    }
}
public class TargetHealingAbility : HealingAbility
{
    public override void Cast()
    {
        base.Cast();
    }
}
public class AOEHealingAbility : HealingAbility
{
    public override void Cast()
    {
        base.Cast();
    }
}

