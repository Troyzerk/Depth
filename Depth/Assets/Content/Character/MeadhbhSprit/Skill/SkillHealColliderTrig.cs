using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHealColliderTrig : MonoBehaviour
{
    [SerializeField] private float coolDown;
    private void Awake()
    {
        StartCoroutine(DestroySkill(coolDown));
    }
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
            target.gameObject.GetComponent<MinionBrain>().DeathCounter(target, heal, new Color32(1, 250, 50, 98));
        }
    }
    private IEnumerator DestroySkill(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
