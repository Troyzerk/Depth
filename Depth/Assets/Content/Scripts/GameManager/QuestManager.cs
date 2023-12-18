using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new();

    // Add methods for starting quests, completing quests, updating objectives, etc.

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
}
