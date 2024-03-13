using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MinionBrain : MonoBehaviour
{
    public GameObject damageCounter;

    public Character minionRef;

    public BattleSceneCtrl _baseGameScript;
    public HealthBarBattleUI _healthBarScript;
    public weaponAnim _weaponAnimScript;

    [SerializeField] GameObject target;
    public float distanceEnemy;
    float timer;
    bool finding = false;

    public bool isAttacking;

    void Start()
    {
        _baseGameScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleSceneCtrl>();
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
            if (target == null)
            {
                DoCheck();
            }
            
            if (target != null)
            {
                distanceEnemy = (transform.position - target.transform.position).sqrMagnitude;

                if (distanceEnemy > minionRef.autoAttackSkill.range)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, minionRef.speed * Time.deltaTime);
                }
                if (distanceEnemy <= minionRef.autoAttackSkill.range && isAttacking == false)
                {
                    isAttacking = true;
                    Character autoBrain = gameObject.GetComponent<MinionBrain>().minionRef;
                    StartCoroutine (CoolDown (autoBrain.autoAttackSkill.cooldown));
                    IsDead(target);
                }
            }
        }
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

        }
        if (this.gameObject.CompareTag("Enemy"))
        {
            target = BattleBehaviour.EnemyToFriendly(this.gameObject);

        }
    }

    public IEnumerator CoolDown(float seconds)
    {
        Character autoBrain = gameObject.GetComponent<MinionBrain>().minionRef;
        yield return new WaitForSeconds(seconds);
        autoBrain.autoAttackSkill.target = target;
        autoBrain.autoAttackSkill.self = gameObject;
        autoBrain.autoAttackSkill.Cast();
        timer -= autoBrain.autoAttackSkill.cooldown;
        isAttacking = false;

    }
}
