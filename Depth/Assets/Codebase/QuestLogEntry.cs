using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

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
        QuestManager.instance.OnQuestUpdateUI += UpdateUI;
        completeButton.SetActive(false);
    }

    private void UpdateUI(object sender, EventArgs e)
    {
        // Grabbing Goal Data
        int currentGoalIndex = PlayerData.instance.quests[questLogEntryIndex].currentGoalIndex;
        List<Goal> goals = PlayerData.instance.quests[questLogEntryIndex].goals;



        // Updating Text
        questTitle.GetComponent<TMP_Text>().text = PlayerData.instance.quests[questLogEntryIndex].questName;
        goalTitle.GetComponent<TMP_Text>().text = goals[currentGoalIndex].goalName;
        questGoalDescription.GetComponent<TMP_Text>().text = goals[currentGoalIndex].description;

        // QuestProgress setting
        questGoalMaxStep.GetComponent<TMP_Text>().text = goals.Count.ToString();
        questGoalProgress.GetComponent<TMP_Text>().text = currentGoalIndex.ToString();

        if (PlayerData.instance.quests[questLogEntryIndex].isCompleted && !completeButton.activeInHierarchy)
        {
            questGoalProgress.GetComponent<TMP_Text>().text = goals.Count.ToString();
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
