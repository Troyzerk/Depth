using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MinionBrain : MonoBehaviour
{
    public GameObject damageCounter;

    public Character minionRef;

    public BaseGameScript _baseGameScript;
    public HealthBarBattleUI _healthBarScript;
    public weaponAnim _weaponAnimScript;

    [SerializeField] GameObject target;
    public float distanceEnemy;
    float timer;
    bool finding = false;

    bool isAttack;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseGameScript>();
        int health = this.gameObject.GetComponent<MinionBrain>().minionRef.health;
        _healthBarScript.SetMaxHealth(health);

    }

    private void Update()
    {
        if (finding) 
        {
            if (target == null) 
            {
                FindTarget();
            }
        }
        if (_baseGameScript.iSee)
        {
            DoCheck();
            if (target != null)
            {
                distanceEnemy = (transform.position - target.transform.position).sqrMagnitude;
                isAttack = false;

                if (distanceEnemy > minionRef.autoAttackSkill.range)
                {
                    
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, minionRef.speed * Time.deltaTime);
                }
                if (distanceEnemy <= minionRef.autoAttackSkill.range)
                {
                    isAttack = true;
                    if (isAttack)
                    {
                        timer += Time.deltaTime;
                        if (timer <=0.1)
                        {
                            _weaponAnimScript.Attack();
                        }
                        if (timer > minionRef.autoAttackSkill.cooldown)
                        {
                            IsAttack(this.gameObject,target);
                            
                            timer -= minionRef.autoAttackSkill.cooldown;
                            isAttack = false;
                            
                        }
                    }
                }
            }
        }
    }

    public void IsAttack(GameObject attacker, GameObject defender)
    {

        int attackStrenght = attacker.gameObject.GetComponent<MinionBrain>().minionRef.autoAttackSkill.damageDelt;

        int health = defender.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;

        DeathCounter(attacker, attackStrenght, new Color32(250, 223,10, 98));

        health -= attackStrenght;
        defender.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;
        defender.transform.GetChild(0).transform.GetChild(0).GetComponent<HealthBarBattleUI>().SetHealth(health);
        IsDead(defender);

    }

    public void DoCheck()
    {
        
        if (BattleBehaviour.TheWinner())
        {
            Debug.Log("Their all dead");
            FindTarget();
            if (gameObject.CompareTag("Minion"))
            {
                _baseGameScript.Victory();
            }
            _baseGameScript.endMenu.SetActive(true);
            _baseGameScript.skillMenu.SetActive(false);
            _baseGameScript.iSee = false;
        }
    }
    public void DeathCounter(GameObject target,int num,Color32 color)
    {
        GameObject DamageInstance = Instantiate(damageCounter, target.transform);

        DamageInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(num.ToString());
        DamageInstance.transform.GetChild(0).GetComponent<TextMeshPro>().color = color;

        if (target.transform.localScale.x <= 0)
        {
            DamageInstance.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        }
    }
    public void IsDead(GameObject target)
    {
        if (target.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth <= 0)
        {
            target.gameObject.GetComponent<MinionBrain>().minionRef.status = CharacterStatus.Dead;
            Object.Destroy(target);

        }
    }
    public void FindTarget()
    {
        int health = this.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;
        finding = true;

        _healthBarScript.SetHealth(health);
        if (this.gameObject.CompareTag("Minion"))
        {
            target = BattleBehaviour.FriendlyToEnemy(this.gameObject);
            if (target != null)
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.red);
            }

        }
        if (this.gameObject.CompareTag("Enemy"))
        {
            target = BattleBehaviour.EnemyToFriendly(this.gameObject);
            if (target != null)
            {
                Debug.DrawLine(transform.position, target.transform.position, Color.green);
            }

        }
    }
}
