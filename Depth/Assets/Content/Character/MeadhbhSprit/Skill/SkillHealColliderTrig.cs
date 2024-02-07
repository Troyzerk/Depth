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
        print(target);
        if (target != null)
        {
            int heal = 1;

            int health = target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
            health += heal;
            target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        }
    }
}
