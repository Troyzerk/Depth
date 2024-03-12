using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

public static class DamageDoneSkill
{
    public static void OnDamageDone(GameObject target,Skill skill)
    {
        
        if (target != null)
        {
            Character charRef = target.GetComponent<MinionBrain>().minionRef;
            int damage = charRef.autoAttackSkill.damage;
            int health = charRef.currentHealth;
            
            if (skill.skillIntent == SkillIntent.Damage)
            {
                
                health -= damage;
            }
            if (skill.skillIntent == SkillIntent.Heal)
            {

                health += damage;
            }

            charRef.currentHealth = health;
            target.transform.GetChild(1).GetComponent<deathCounter_Ctrl>().SpawnCounter(target, damage, skill.color);
            target.GetComponent<MinionBrain>().IsDead(target);
            target.transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBarBattleUI>().SetHealth(health);
        }
    }
}
