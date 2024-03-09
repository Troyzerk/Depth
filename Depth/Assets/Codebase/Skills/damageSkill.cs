using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public enum TargetType
{
    Passive,
    Aggressive
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/DamageSkill")]
public class damageSkill : Skill
{
    public Color32 color;
    public Character charRef;

    public int tickDamage;
    public TargetType targetType;
    public void OnDamageDone(GameObject target)
    {
        if (target != null)
        {
            charRef = target.GetComponent<MinionBrain>().minionRef;
            int damage = this.damage;

            int health = charRef.currentHealth;
            health -= damage;
            charRef.currentHealth = health;
            target.transform.GetChild(1).GetComponent<deathCounter_Ctrl>().SpawnCounter(target, damage, color);
            target.GetComponent<MinionBrain>().IsDead(target);
            target.transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBarBattleUI>().SetHealth(health);
        }
    }
    public override IEnumerator OverTime(GameObject self, GameObject target)
    {

        while (self.gameObject.GetComponent<SkillPerfab>().isDamage)
        {
            yield return new WaitForSeconds(tickDamage);
            this.OnDamageDone(target);
        }
       
    }
}
