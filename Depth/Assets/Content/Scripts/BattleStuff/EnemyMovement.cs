using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public float closestDistance;
    public float enemyDistance;

    private List<Character> minionList;

    GameObject _EnemyGO;
   
    public GameObject founder;
    
    public BaseGameScript _baseGameScript;

    public int attackDistance;

    public float attackDelay;
    public float attackDelayReset;

    public int health;
    public int attackStrenght;
    private bool isAttacking;

    public GameObject hitCounter;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseGameScript>();


    }

    void FixedUpdate()
    {
        if (_baseGameScript.iSee)
        {
            FindClosestEnemy();
        }
    }

    void FindClosestEnemy()
    {      
        closestDistance = Mathf.Infinity;
        minionList = GlobalHolder.playerPartyReference.characters;  
        MinionBrain enemycharref = this.gameObject.GetComponent<MinionBrain>();

        foreach (Character enemy in minionList)
        {
            
            if (GameObject.Find(enemy.name) != null)
            {   
                _EnemyGO = GameObject.Find(enemy.name);
                enemyDistance = (_EnemyGO.transform.position - this.transform.position).sqrMagnitude;
                if (enemyDistance < closestDistance)
                {
                    closestDistance = enemyDistance;
                    founder = GameObject.Find(enemy.name);
                }
            }
        }
        Debug.DrawLine(this.transform.position,founder.transform.position,Color.red);

        attackDistance = this.gameObject.GetComponent<MinionBrain>().minionRef.autoAttackSkill.range;
        attackDelay = this.gameObject.GetComponent<MinionBrain>().minionRef.autoAttackSkill.cooldown;
        health = founder.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;

        if (closestDistance >= attackDistance)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, founder.transform.position, enemycharref.minionRef.speed * Time.deltaTime);
        }
        if (closestDistance <= attackDistance)
        {
            if (!isAttacking)
            {
                print("ouch");
                StartCoroutine(doCheck(attackDelay, founder, this.gameObject, health));

            }

        }

    }



    IEnumerator doCheck(float cooldown, GameObject attackingMin, GameObject attackerMin, int health)
    {
        isAttacking = true;

        attackStrenght = this.gameObject.GetComponent<MinionBrain>().minionRef.autoAttackSkill.damageDelt;

        health -= attackStrenght;
        founder.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;


        if (health >= 0)
        {
            Destroy(founder);
            print("Destroyed");
        }
        isAttacking = false;
        yield return new WaitForSeconds(cooldown);

    }
}

