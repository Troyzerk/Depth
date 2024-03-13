using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/MeleeSkill")]

public class MeleeAttackSkill : AutoAttackSkill
{
    public override void Cast()
    {
        MinionFunctionality.AttackIt(target, self, cooldown, damage);

        base.Cast();
    }

}
