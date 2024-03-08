using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/MeleeSkill")]

public class MeleeAttackSkill : AutoAttackSkill
{
    public GameObject target;
    public GameObject self;

    public void Attacking()
    {
        int attackStrenght = damageDealt;

        int health = self.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;

        target.transform.GetChild(1).GetComponent<deathCounter_Ctrl>().SpawnCounter(target, attackStrenght, new Color32(250, 223, 10, 98));

        health -= attackStrenght;
        self.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        self.transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBarBattleUI>().SetHealth(health);
    }

}
