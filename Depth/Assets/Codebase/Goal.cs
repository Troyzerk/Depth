using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;


/*
 * 
 * Master class for Goals
 * only do singular things inside of this class.
 * 
 * 
 */
public enum GoalType
{
    DefeatParty,
    CollectLandmark,
    CollectGoldLandmark,
    CollectExpLandmark,
    CollectRecruitLandmark,

}
public class Goal
{
    public GoalDiscriptor discriptor;
    public GoalType type;
    public bool isCompleted;

    public void Awake()
    {
        Init();
        if (discriptor == null)
        {
            Debug.LogError("Discriptor could not be assigned on quest : " + this);
        }
    }
    public virtual void ProgressGoal()
    {

    }
    public virtual void CompleteGoal()
    {

    }
    public virtual void Init()
    {
        GenerateGoalDiscriptor(type);
    }
    public GoalDiscriptor GenerateGoalDiscriptor(GoalType goalType)
    {
        GoalDiscriptor[] goalDiscriptors = Resources.LoadAll("Quests/GoalDescriptors", typeof(GoalDiscriptor)).Cast<GoalDiscriptor>().ToArray();

        foreach (var goalDiscriptor in goalDiscriptors)
        {
            if(goalDiscriptor.type == goalType)
            {
                return goalDiscriptor;
            }
        }
        Debug.LogError("NO GOALS OF TYPE : " + goalType);
        return null;
    }

}
