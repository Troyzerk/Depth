using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
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
    DefeatEnemy,
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
    public bool hasSteps;
    public int currentAmount;
    public int amount;

    public void Awake()
    {
        Init();
        if (discriptor == null)
        {
            Debug.LogError("Discriptor could not be assigned on quest : " + this);
        }
    }
    public virtual void Init()
    {
        if(discriptor == null || discriptor.type != type)
        {
            GenerateGoalDiscriptor(type);
        }
        
        if (amount <= 0)
        {
            amount = 1;
        }
        currentAmount = 0;
    }

    //--------------------------------------------------------- Activity
    public virtual void ProgressGoal()
    {
        currentAmount++;
        Debug.Log("Progressing Goal!");
        if (currentAmount >= amount)
        {
            CompleteGoal();
            Debug.Log("Goal Completed!");
        }
        else
        {
            isCompleted = false;
        }
        QuestManager.instance.UpdateAllQuestData();
    }
    public virtual void CompleteGoal()
    {
        isCompleted = true;
    }
    
    public void GenerateGoalDiscriptor(GoalType goalType)
    {
        GoalDiscriptor[] goalDiscriptors = Resources.LoadAll("Quests/GoalDescriptors", typeof(GoalDiscriptor)).Cast<GoalDiscriptor>().ToArray();
        List<GoalDiscriptor> newList = new();

        

        foreach (var goalDisc in goalDiscriptors)
        {
            if(goalDisc.type == goalType)
            {
                newList.Add(goalDisc);
            }
        }

        foreach (var goalDiscriptor in newList)
        {
            if(newList.Count <= 0)
            {
                Debug.LogError("NO GOALS OF TYPE : " + goalType);
            }
            
            var selectedDisc = newList[Random.Range(0, newList.Count)];
            if(selectedDisc.type != goalType)
            {
                Debug.LogWarning($"Found mismatch of goal Discriptor -------> {selectedDisc}");
            }
            else
            {
                discriptor = selectedDisc;
            }
        }
    }

}
