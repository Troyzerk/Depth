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
        currentGoalIndex++;
        if (currentGoalIndex >= goals.Count)
        {
            isGoalsCompleted = true;
            currentGoalIndex = goals.Count - 1;
            QuestFinished();
        }
    }

    public void CheckGoalProgress()
    {
        if (goals[currentGoalIndex].isCompleted)
        {
            ProgressQuest();
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
            currentGoalIndex = goals.Count - 1;
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
                int randomClass = Random.Range(0, 2);
                //Debug.Log(randomClass);
                switch (randomClass)
                {
                    case 0:                     
                        goals.Add(new DefeatGoal());
                        if(QuestManager.instance.questManagerDebugs) Debug.Log("adding new defeat goal");
                        break;
                    case 1:
                        goals.Add(new CollectLandmarkGoal());
                        if (QuestManager.instance.questManagerDebugs) Debug.Log("adding new collect landmark goal");
                        break;
                    case 2:
                        goals.Add(new CollectLandmarkGoal());
                        if (QuestManager.instance.questManagerDebugs) Debug.Log("adding new collect landmark goal");
                        break;
                    case 3:
                        goals.Add(new CollectLandmarkGoal());
                        break;
                    case 4:
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
                    goal.GenerateGoalDiscriptor(goal.type);
                    goal.Init();
                }
            }
            currentGoalIndex = 0;
        }
    }
}