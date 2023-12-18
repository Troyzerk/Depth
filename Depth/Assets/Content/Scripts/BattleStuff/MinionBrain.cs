using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MinionBrain : MonoBehaviour
{
    public Character minionRef;
    public BaseGameScript _baseGameScript;
    [SerializeField] GameObject target;
    public List<Character> playerParty = new();
    public List<Character> NPCParty = new();
    public float distanceEnemy;
    float timer;

    bool isAttack;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseGameScript>();
        playerParty = PersistentManager.instance.playerParty.characters;
        NPCParty = PersistentManager.instance.enemyParty.characters;

    }

    private void Update()
    {
        if (_baseGameScript.iSee)
        {
            if (this.gameObject.CompareTag("Minion"))
            {
                DoCheck(NPCParty);
                target = BattleBehaviour.FindClosestEnemy(PersistentManager.instance.enemyParty.characters, this.gameObject);
                if (target != null)
                {
                    Debug.DrawLine(transform.position, target.transform.position, Color.red);
                }

            }
            if (this.gameObject.CompareTag("Enemy"))
            {
                DoCheck( playerParty);
                target = BattleBehaviour.FindClosestEnemy(PersistentManager.instance.playerParty.characters, this.gameObject);
                if (target != null)
                {
                    Debug.DrawLine(transform.position, target.transform.position, Color.green);
                }

            }

            if (target != null)
            {
                distanceEnemy = (transform.position - target.transform.position).sqrMagnitude;

                if (distanceEnemy > minionRef.autoAttackSkill.range)
                {
                    isAttack = false;
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, minionRef.speed * Time.deltaTime);
                }
                if (distanceEnemy <= minionRef.autoAttackSkill.range)
                {
                    isAttack = true;
                    if (isAttack)
                    {
                        timer += Time.deltaTime;
                        if (timer > minionRef.autoAttackSkill.cooldown)
                        {
                            IsAttack(target, this.gameObject);
                            
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

        health -= attackStrenght;
        defender.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;

        if (health >= 0)
        {
            defender.gameObject.GetComponent<MinionBrain>().minionRef.status = CharacterStatus.Dead;
            Object.Destroy(defender);
        }

    }

    public void DoCheck(List<Character> attacker)
    {
        int killCount = 0;
        for (int i = 0; i < attacker.Count; i++)
        {
            if (GameObject.Find(attacker[i].name) == null)
            {
                killCount++;
            }
        }
        if (killCount == attacker.Count)
        {
            Debug.Log("Their all dead");
            if (gameObject.CompareTag("Minion"))
            {
                _baseGameScript.Victory();
            }
            _baseGameScript.endMenu.SetActive(true);
            _baseGameScript.skillMenu.SetActive(false);
            _baseGameScript.iSee = false;
        }
    }
}
