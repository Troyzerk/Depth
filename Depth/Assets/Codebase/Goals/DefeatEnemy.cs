using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatEnemy : Goal
{
    public override void Init()
    {
        type = GoalType.DefeatEnemy;
        amount = Random.Range(1,7);
        base.Init();
    }
}
