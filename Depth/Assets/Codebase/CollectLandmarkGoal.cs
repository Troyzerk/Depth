using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This Goal Class is to set generic collect goals for the player. 
 * 
 * User needs to set the amount to collect in the class. 
 * 
 */

public class CollectLandmarkGoal : Goal
{
    public int amountToCollect;
    public override void Init()
    {

        type = GoalType.CollectLandmark;
        base.Init();
    }
    public override void ProgressGoal()
    {

    }
}
