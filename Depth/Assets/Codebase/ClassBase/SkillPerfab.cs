using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
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

    public List<GameObject> charsInPool = new();

    GameObject target;
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
            target = col.gameObject;
            if (!charsInPool.Contains(target))
            {
                if (target.CompareTag("Minion")|| target.CompareTag("Enemy"))
                {
                    print(col.gameObject);
                    charsInPool.Add(target);
                }
                else
                {
                    charsInPool.Add(target.transform.parent.gameObject);
                }
            }
            isDamage = true;
            StartCoroutine(OverTime());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isPlaced)
        {
            isDamage = false;
            charsInPool.Clear();
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
