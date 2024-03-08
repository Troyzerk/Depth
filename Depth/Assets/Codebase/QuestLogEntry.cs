using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;
using UnityEditor;

/*
 *  This script should be attached to the QuestLogEntry Prefab and subscribe to the UI Events that get called
 *  in the HUD.
 * 
 * 
 */
public class QuestLogEntry : MonoBehaviour
{
    public int questLogEntryIndex;

    [SerializeField]
    GameObject questTitle,goalTitle,questGoalMaxStep,questGoalDescription,questGoalProgress,completeButton;

    public void Awake()
    {
        // subscribes the UpdateUI function to the OnQuestUpdate event handler
        print(PlayerData.instance.quests[questLogEntryIndex].CheckCurrentGoal());
        QuestManager.instance.OnQuestUpdateUI += UpdateQuestUI;
        completeButton.SetActive(false);
    }

    private void UpdateQuestUI(object sender, EventArgs e)
    {
        //Update the quest log instance to the current goal data.
        
        Goal goal = PlayerData.instance.quests[questLogEntryIndex].CheckCurrentGoal();
        Quest quest = PlayerData.instance.quests[questLogEntryIndex];
        print(PlayerData.instance.quests[questLogEntryIndex].CheckCurrentGoal());
        if (goal.discriptor == null)
        {
            Debug.LogError("Discriptor has gotten Lost");
        }

        questTitle.GetComponent<TMP_Text>().text = quest.questName;
        goalTitle.GetComponent<TMP_Text>().text = goal.discriptor.goalName;
        questGoalMaxStep.GetComponent<TMP_Text>().text = quest.goals.Count.ToString();
        questGoalDescription.GetComponent<TMP_Text>().text = goal.discriptor.goalDescription;
        questGoalProgress.GetComponent<TMP_Text>().text = quest.currentGoalIndex.ToString();

        if (quest.isCompleted)
        {
            questGoalProgress.GetComponent<TMP_Text>().text = quest.goals.Count.ToString();
            completeButton.SetActive(true);
        }
        else
        {
            completeButton.SetActive(true);
        }


    }


    public void CompleteButtonPressed()
    {
        if (PlayerData.instance.quests[questLogEntryIndex].isCompleted)
        {
            PlayerData.instance.quests[questLogEntryIndex].GiveReward();
            PlayerData.instance.quests.RemoveAt(questLogEntryIndex);
            Destroy(gameObject);
        }
    }
}
