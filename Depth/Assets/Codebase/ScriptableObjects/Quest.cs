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
    public bool isGoalsCompleted;
    public bool isPopulated;
    
    public virtual void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        questProgressCurrentStep = 0;
        currentGoalIndex = 0;
        isCompleted = false;
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
        Debug.Log("Progressing quest");
        currentGoalIndex++;
        if (currentGoalIndex >= goals.Count)
        {
            Debug.Log("Progressing quest but quest is finished");
            isGoalsCompleted = true;
            currentGoalIndex = goals.Count -1;
            QuestFinished();
        }
    }

    public Goal CheckCurrentGoal()
    {
        if ( currentGoalIndex >= goals.Count)
        {
            isCompleted = true;
            return goals[goals.Count -1];
        }
        else
        {

            return goals[currentGoalIndex];
        }
    }
    public void QuestFinished()
    {
        if (isGoalsCompleted)
        {
            isCompleted = true;
        }
    }
    public void PopulateQuestWithRandomGoals(int goalAmount)
    {
        if (isPopulated)
        {
            Debug.LogWarning("Goal List in Quest Already Populated");
        }
        else
        {
            for (int i = 0; i < goalAmount; i++)
            {
                int randomClass = Random.Range(0, 1);
                switch (randomClass)
                {
                    case 0:                     
                        goals.Add(new DefeatGoal());
                        break;
                    case 1:
                        goals.Add(new CollectLandmarkGoal());
                        break;
                }
                
            }

            //Debug.Log("Number of goals in " + questName + " : " + goals.Count + " ");
            if (goals.Count <= 0)
            {
                Debug.LogError("Goals.Count : " + goals.Count + " : population of list failed.");
                Debug.LogError(goalAmount);
            }

            foreach (Goal goal in goals)
            {
                if(goal.discriptor == null)
                {
                    goal.discriptor = goal.GenerateGoalDiscriptor(goal.type);
                }
            }
            currentGoalIndex = 0;
        }
    }
}