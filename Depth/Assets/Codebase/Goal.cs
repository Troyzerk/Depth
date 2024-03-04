using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Video;

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
    }
    public virtual void ProgressGoal()
    {

    }
    public virtual void CompleteGoal()
    {

    }
    public virtual void Init()
    {
        Generate(type);
    }
    public void Generate(GoalType goalType)
    {
        GoalDiscriptor[] goalDiscriptors = Resources.LoadAll("Quests/GoalDescriptors", typeof(GoalDiscriptor)).Cast<GoalDiscriptor>().ToArray();
        List<GoalDiscriptor> newGoalDiscriptors = new();
        foreach (var goalDiscriptor in goalDiscriptors)
        {
            if(goalDiscriptor.type == goalType)
            {
                newGoalDiscriptors.Add(goalDiscriptor);
            }
        }
        

        if (newGoalDiscriptors.Count <= 0)
        {
            Debug.LogWarning("No goals of type : [" + goalType + "] exist. Please add some.");
        }
        else
        {
            discriptor = newGoalDiscriptors[Random.Range(0, newGoalDiscriptors.Count)];
        }

        
    }

}
