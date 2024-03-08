using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/HealSkill")]
public class HealSkill : Skill
{
    public Color32 color;
    public TargetType TargetType = TargetType.Aggressive;
    public int tickDamage;
    public void OnDamageDone(GameObject target)
    {
        if (target != null)
        {
            int damage = this.damage;

            int health = target.GetComponent<MinionBrain>().minionRef.currentHealth;
            health += damage;
            target.GetComponent<MinionBrain>().minionRef.currentHealth = health;
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
/*
 * public override void Awake()
    {
        PlayerData.instance.playerCharacter.health += damage;
        base.Awake();
    }
*/