using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;


public class Quest
{
    public List<Goal> goals = new();
    public int currentGoalIndex;


    public string questName;
    public string description;


    public int questProgressCurrentStep, experienceReward, goldReward, characterReward;

    public bool isCompleted;
    public bool isPopulated;


    public virtual void CheckGoals()
    {
        
    }

    public virtual void GiveReward()
    {
        /*
         * We will have to polish this more when the combatEvent system
         * is up and running. 
         */
    }
    public virtual void ProgressQuest()
    {
        currentGoalIndex++;
    }

    public Goal CheckCurrentGoal()
    {
        return goals[currentGoalIndex];
    }
    public void PopulateQuestWithRandomGoals(int goalAmount)
    {
        if (isPopulated)
        {
            Debug.LogWarning("Goal List in Quest Already Populated");
        }
        else
        {
            goals.Clear();
            for (int i = 0; i < goalAmount; i++)
            {
                int randomClass = Random.Range(0, 2);
                switch (randomClass)
                {
                    case 0:
                        goals.Add(new DefeatGoal());
                        Debug.Log(goals.Count + " : SWITCH Output case 0");
                        break;
                    case 1:
                        goals.Add(new CollectLandmarkGoal());
                        Debug.Log(goals.Count + " : SWITCH Output case 1");
                        break;
                    case 2:
                        goals.Add(new DefeatGoal());
                        Debug.Log(goals.Count + " : SWITCH Output case 2");
                        break;
                }
                Debug.Log(goals.Count + " : After switch output");
            }
            if (goals.Count <= 0)
            {
                Debug.LogError("Goals.Count : " + goals.Count + " : population of list failed.");
                Debug.LogError(goalAmount);
            }


            foreach (Goal goal in goals)
            {
                goal.Generate(goal.type);
                if (!goal.discriptor)
                {
                    Debug.LogError("Goal discriptor is null.");
                }
            }
        }
        
    }
}


