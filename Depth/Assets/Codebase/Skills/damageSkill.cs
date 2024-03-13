using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public enum TargetType
{
    Passive,
    Aggressive
}

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/DamageSkill")]
public class damageSkill : Skill
{
    public override void Cast()
    {
        base.Cast();
    }
}
