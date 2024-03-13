using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This Goal Class is to set generic defeat goals for the player. 
 * 
 * 
 */

public class DefeatGoal : Goal
{
    public override void Init()
    {
        type = GoalType.DefeatParty;
        base.Init();
    }
    public override void ProgressGoal()
    {

    }
}
