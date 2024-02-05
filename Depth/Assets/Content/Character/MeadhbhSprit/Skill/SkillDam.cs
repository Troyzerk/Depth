using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDam : MonoBehaviour
{
    public GameObject _selected;
    void OnTriggerEnter2D(Collider2D col)
    {
        
        
        if (col.gameObject.CompareTag("Minion") )
        {
            return;
        }
        if (col.gameObject.CompareTag("Enemy" ))
        {
            _selected = col.gameObject;
        }

        int health = _selected.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
        health -= 1;
        _selected.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
    }
}
