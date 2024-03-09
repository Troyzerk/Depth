using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public static class MinionFunctionality
{
    
    public static void AttackIt(GameObject target, GameObject self,float cooldown,int damage)
    {
        Debug.Log("Attacking");
        self.transform.GetChild(3).GetComponent<Animator>().SetTrigger("Attack");
        
        int attackStrenght = damage;

        int health = target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
        target.transform.GetChild(1).GetComponent<deathCounter_Ctrl>().SpawnCounter(target, attackStrenght, new Color32(250, 223, 10, 98));

        health -= attackStrenght;
        target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        target.transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBarBattleUI>().SetHealth(health);
    }
}
