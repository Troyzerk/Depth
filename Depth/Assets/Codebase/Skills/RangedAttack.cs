using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/RangedSkill")]
public class RangedAttack : AutoAttackSkill
{
    public override void Cast()
    {
        Debug.Log("Rangeskill Skill Casting");
        MinionFunctionality.AttackIt(target, self, cooldown, damage);

        base.Cast();
    }
}
