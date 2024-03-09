using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/AutoAttackSkill")]
public class AutoAttackSkill : Skill
{
    public int damageDealt;

    public GameObject target;
    public GameObject self;

    public GameObject weapon;
}
