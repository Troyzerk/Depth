using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * This Goal Class is to set generic defeat goals for the player. 
 * 
 * 
 */

[CreateAssetMenu(fileName = "NewGoal", menuName = "Questing/NewDefeatGoal")]
public class DefeatGoal : Goal
{
    public override void Awake()
    {
        base.Awake();
        //subscribe progressgoal function to an event in the battleResolutionManager
    }
    public override void ProgressGoal()
    {

    }
}
