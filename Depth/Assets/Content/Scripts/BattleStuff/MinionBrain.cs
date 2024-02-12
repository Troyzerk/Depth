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
    public List<Character> playerParty = new();
    public List<Character> NPCParty = new();
    public Character mainCharacter;
    public float distanceEnemy;
    float timer;

    bool isAttack;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BaseGameScript>();
        //_healthBarScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<HealthBarBattleUI>();
        playerParty = PersistentManager.instance.playerParty.characters;
        NPCParty = PersistentManager.instance.enemyParty.characters;
        mainCharacter =PersistentManager.instance.playerCharacter;
        int health = this.gameObject.GetComponent<MinionBrain>().minionRef.health;
        if (mainCharacter != null )
        {
            playerParty.Add(mainCharacter);
        }
        _healthBarScript.SetMaxHealth(health);

    }

    private void Update()
    {
        if (_baseGameScript.iSee)
        {
            int health = this.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth;

            _healthBarScript.SetHealth(health);
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
                            print("Wack");
                        }
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

        _healthBarScript.SetHealth(health);

        DeathCounter(attacker, attackStrenght, new Color32(250, 223,10, 98));
        print("Hit");
        health -= attackStrenght;
        defender.gameObject.GetComponent<MinionBrain>().minionRef.currentHealth = health;

        if (health <= 0)
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
}
