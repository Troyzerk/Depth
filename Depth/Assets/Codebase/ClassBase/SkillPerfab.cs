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
    public bool isActive = false;
    public bool isPlaced = false;

    List<GameObject> charsInPool = new();

    GameObject enemyTarget;
    [SerializeField] GameObject collisionObject;
    

    private void Update()
    {
        if (isActive)
        {
            Placed();
        }
    }
    /*
     *  Collisions
     */
    void OnTriggerEnter2D(Collider2D col)
    {
        
        if(isPlaced)
        {
            if (!charsInPool.Contains(col.gameObject))
            {
                charsInPool.Add(col.gameObject);
            }
            enemyTarget = col.gameObject;
            isDamage = true;
            skill.target = col.gameObject;
            StartCoroutine(OverTime());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlaced)
        {
            isDamage = false;
        }
        
    }
    /*
     *  ENums 
     */
    public IEnumerator DeActivate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Deactivated();
    }
    public IEnumerator OverTime()
    {
        yield return new WaitForSeconds(skill.tickDamage);
        for(int i = 0; i< charsInPool.Count; i++)
        {
            DamageDoneSkill.OnDamageDone(charsInPool[i],skill);
        }
        
    }

    public void Activated()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        isDamage = false;
        isActive = true;
    }

    public void Placed()
    {
        if (!isPlaced)
        {
            isPlaced = true;
            StartCoroutine(DeActivate(skill.cooldown));
        }
    }

    public void Deactivated()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        transform.position = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        isDamage = false;
        isActive = false;
        isPlaced= false;
    }

}
