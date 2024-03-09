using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/RangedSkill")]
public class RangedAttack : AutoAttackSkill
{
    public override void Cast()
    {
        MinionFunctionality.AttackIt(target, self, cooldown, damage);

        base.Cast();
    }
}
