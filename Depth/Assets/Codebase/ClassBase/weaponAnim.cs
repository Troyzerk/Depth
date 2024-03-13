using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnim : MonoBehaviour
{   
    //public Animator animator;
    public float delay = 0.3f;
    public void Attack()
    {
        print(GetComponent<Animator>().name);

        gameObject.GetComponent<Animator>().SetTrigger("Attack");
    }
}
