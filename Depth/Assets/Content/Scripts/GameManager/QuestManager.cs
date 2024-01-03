using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new();
    public Quest startingQuest;
    // Add methods for starting quests, completing quests, updating objectives, etc.

    public void Start()
    {
        quests.Clear();
        quests.Add(startingQuest);
        startingQuest.currentGoal = startingQuest.questGoals[0];
        UpdateQuestLogUI();
    }

    public void StartQuest(string questName)
    {
        Quest quest = quests.Find(q => q.questName == questName);
        if (quest != null && !quest.isCompleted)
        {
            Debug.Log("Quest started: " + questName);
            // Additional logic for initializing the quest.
        }
    }

    public void CompleteQuest(string questName)
    {
        Quest quest = quests.Find(q => q.questName == questName);
        if (quest != null && !quest.isCompleted)
        {
            quest.isCompleted = true;
            Debug.Log("Quest completed: " + questName);
            // Additional logic for quest completion and rewards.
        }
    }

    public void UpdateQuestLogUI()
    {
        GameObject questEntry = GameObject.Find("HUD/QuestLog/QuestEntry");
        if (questEntry != null)
        {
            GameObject questTitle = GameObject.Find("HUD/QuestLog/QuestEntry/QuestTitle");
            GameObject questGoalDescription = GameObject.Find("HUD/QuestLog/QuestEntry/QuestGoalDescription");
            GameObject questGoalProgress = GameObject.Find("HUD/QuestLog/QuestEntry/QuestGoalProgress");

            questTitle.GetComponent<TMP_Text>().text = startingQuest.questName;
            questGoalDescription.GetComponent<TMP_Text>().text = startingQuest.currentGoal.description;
            questGoalProgress.GetComponent<TMP_Text>().text = startingQuest.currentGoal.currentAmount.ToString() + "/" + startingQuest.currentGoal.requiredAmount.ToString();
            if (!startingQuest.isCompleted)
            {
                GameObject questCompleteButton = GameObject.Find("HUD/QuestLog/QuestCompleteButton");
                questCompleteButton.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("No QuestEntry Found!");
        }
    }
}
