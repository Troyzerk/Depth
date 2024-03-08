using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DeathCounterAnim : MonoBehaviour
{
    public Animator animator;
    AnimatorStateInfo stateInfo;
    public float lenght;
    private void Update()
    {
        if (animator != null)
        { 
            stateInfo = transform.GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);

            if (stateInfo.normalizedTime > .8f)
            {
                Decay();
            }
        }
    }

    public void Attack()
    {
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        animator.SetTrigger("Damage");
    }

    public void Decay() 
    {
        gameObject.SetActive(false);
    }
}
