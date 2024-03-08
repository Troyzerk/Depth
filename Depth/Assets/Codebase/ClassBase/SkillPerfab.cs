using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillPerfab : MonoBehaviour
{
    public Skill skill;
    //public HealingSkill hSkill;
    
    public bool isDamage = false;

    GameObject enemyTarget;
    void OnTriggerEnter2D(Collider2D col)
    {
        enemyTarget = col.gameObject;
        isDamage = true;
        TriggerSkill(col.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isDamage = false;
    }
    public void TriggerSkill(GameObject target)
    {
        StartCoroutine(skill.OverTime(gameObject, target));
        StartCoroutine(DeActivate(skill.cooldown));        
    }

    public IEnumerator DeActivate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        isDamage = false;

    }

}
