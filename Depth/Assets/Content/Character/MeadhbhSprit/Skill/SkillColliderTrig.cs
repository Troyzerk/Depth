using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillColliderTrig : MonoBehaviour
{
    [SerializeField] private float coolDown, repeatTrigger;
    private void Awake()
    {
        StartCoroutine(DestroySkill(coolDown));
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy" ))
        {
            GameObject enemyTarget= col.gameObject;
            StartCoroutine(BurnBitch(repeatTrigger, enemyTarget));
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
            target.gameObject.GetComponent<MinionBrain>().DeathCounter(target,damage,new Color32(250,34,0,98));
        }
    }

    private IEnumerator DestroySkill(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

    private IEnumerator BurnBitch(float seconds, GameObject target)
    {
        yield return new WaitForSeconds(seconds); 
        DealDamage(target);
    }
}
