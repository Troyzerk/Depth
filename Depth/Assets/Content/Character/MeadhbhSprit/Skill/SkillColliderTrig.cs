using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillColliderTrig : MonoBehaviour
{
    public SpellScript _spellScript;

    private void Start()
    {
        _spellScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpellScript>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Minion") )
        {
            _spellScript.friendlyTarget = col.gameObject;
        }
        if (col.gameObject.CompareTag("Enemy" ))
        {
            GameObject enemyTarget= col.gameObject;
            DealDamage(enemyTarget);
        }
    }
    public void DealDamage(GameObject target)
    {
        if (target != null)
        {
            int damage = 1;

            int health = target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
            health -= damage;
            target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        }
    }
    public void DealHeal(GameObject target)
    {
        if (target != null)            
        {
            int heal = 1;
            int health = target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
            health += heal;
            target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        }
    }
}
