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
    public Quest questRef;

    [SerializeField] 
    public int questCount;
    public int questCurrentStep;
    public List<GoalType> goalTypes = new();

    [SerializeField]
    GameObject questTitle,goalTitle,questGoalMaxStep,questGoalDescription,questGoalProgress,completeButton;

    public void Awake()
    {
        // subscribes the UpdateUI function to the OnQuestUpdate event handler
        QuestManager.instance.OnQuestUpdateUI += UpdateQuestUI;
        completeButton.SetActive(false);

    }

    private void UpdateQuestUI(object sender, EventArgs e)
    {
        goalTypes.Clear();
        questCount = questRef.goals.Count;
        questCurrentStep = questRef.currentGoalIndex;

        foreach (Goal goal in questRef.goals)
        {
            goalTypes.Add(goal.type);
        }


        if (questLogEntryIndex > PlayerData.instance.quests.Count - 1)
        {
            Debug.Log("QuestLogEntryIndex is more then the count of quests in the list.");
            // this means that the quest no longer exists in the array and should of been destroyed. 
        }
        else
        {
            //Update the quest log instance to the current goal data.
            Goal goal = PlayerData.instance.quests[questLogEntryIndex].CheckCurrentGoal();
            Quest quest = PlayerData.instance.quests[questLogEntryIndex];

            questTitle.GetComponent<TMP_Text>().text = quest.questName;
            goalTitle.GetComponent<TMP_Text>().text = goal.discriptor.goalName;
            questGoalMaxStep.GetComponent<TMP_Text>().text = quest.goals.Count.ToString();
            questGoalDescription.GetComponent<TMP_Text>().text = goal.discriptor.goalDescription;
            questGoalProgress.GetComponent<TMP_Text>().text = quest.currentGoalIndex.ToString();

            if (quest.isCompleted)
            {
                questGoalProgress.GetComponent<TMP_Text>().text = PlayerData.instance.quests[questLogEntryIndex].goals.Count.ToString();
                completeButton.SetActive(true);
            }
            else
            {
                completeButton.SetActive(false);
            }
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
        else
        {
            Debug.LogError("Complete button active but the quest is not completed.");
        }
    }
}
