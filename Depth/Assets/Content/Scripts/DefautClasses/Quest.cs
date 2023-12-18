using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Questing/NewQuest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public int experienceReward;
    public List<QuestGoal> questGoals = new();
    public bool isCompleted;

    public Quest(string name, string desc)
    {
        questName = name;
        description = desc;
        isCompleted = false;
    }

    public void CheckGoals()
    {
        isCompleted = questGoals.All(g => g.isQuestGoalCompleted);
        if (isCompleted) GiveReward();
    }

    void GiveReward()
    {
        /*
         * We will have to polish this more when the combatEvent system
         * is up and running. 
         */
    }
}

/*
 * this quest goal class holds all the goals of a quest. 
 * a quest is just a container for all the goals. 
 * once all the quest goals are completed then we call the quest complete 
 */
[System.Serializable]
public class QuestGoal
{
    public bool isQuestGoalCompleted;
    public int requiredAmount;
    public int currentAmount;
    public string description;
    
    public virtual void Init()
    {
        //Default init stuff
    }


    public void Evaluate()
    {
        if (currentAmount >= requiredAmount)
        {
            Complete();
        }
    }
    public void Complete()
    {
        isQuestGoalCompleted = true;
    }
}


//https://www.youtube.com/watch?v=h7rRic4Xoak//

/*
 * This does not kick off yet
 * we need to add a combat event tracker for the combat
 * once that is done we add the combatevent.onenemyDeath here and add the EnemyDied Function 
 * to it. 
 */
[System.Serializable]
public class KillQuestGoal: QuestGoal
{
    public RaceID raceID;
    public void KillGoal(RaceID raceID, string description, bool isCompleted, int currentAmount, int requiredAmount)
    {
        this.raceID = raceID;
        this.description = description;
        this.isQuestGoalCompleted = isCompleted;
        this.requiredAmount = requiredAmount;
        this.currentAmount = currentAmount;
    }
    
    public override void Init()
    {
        base.Init();
    }
    void EnemyDied(RaceID raceID)
    {
        if(raceID == this.raceID)
        {
            currentAmount++;
        }
    }
} 

