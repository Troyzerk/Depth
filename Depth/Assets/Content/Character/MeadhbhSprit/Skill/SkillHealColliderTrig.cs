using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHealColliderTrig : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Minion") )
        {
            GameObject HeroTarget = col.gameObject;
            DealHeal(HeroTarget);
        }
    }
    public void DealHeal(GameObject target)
    {
        if (target != null)
        {
            int heal = 1;

            int health = target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
            int maxhealth = target.gameObject.GetComponent<MinionBrain>().minionRef.health;
            
            if (health < maxhealth)
            {
                health += heal;
                target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
                target.gameObject.GetComponent<MinionBrain>().DeathCounter(target, heal, new Color32(1, 250, 50, 98));
                target.transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBarBattleUI>().SetHealth(health);
            }
            
        }
    }
}
