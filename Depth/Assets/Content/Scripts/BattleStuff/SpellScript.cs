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
