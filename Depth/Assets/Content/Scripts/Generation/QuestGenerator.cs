using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuestGenerator
{
   
    public static Quest GenerateQuest(string name,string description ,QuestGoalType questGoalType, int steps)
    {
        Quest quest = ScriptableObject.CreateInstance<Quest>();
        quest.description = description;
        quest.name = name;
        quest.questName = name;

        for (int i = 0;i< steps; i++)
        {
            quest.questGoals.Add(GenerateStep(questGoalType));
        }

        return quest;
    }

    public static QuestGoal GenerateStep(QuestGoalType questGoalType)
    {
        QuestGoal goal;
        goal = new QuestGoal();
        if (questGoalType == QuestGoalType.Kill)
        {
            goal = new KillQuestGoal();
        }
        else if (questGoalType == QuestGoalType.Find)
        {
            goal = new FindQuestGoal();
        }
        else if (questGoalType == QuestGoalType.Visit)
        {

        }

        goal.Init();
        
        return goal;
    }
}
