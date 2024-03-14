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
    bool firstLoad = true;

    [SerializeField]
    public Quest questRef;
    public Goal currentGoalRef;

    [SerializeField] 
    public int questCount;
    public int questCurrentStep;
    public int goalAmount;
    public int goalCurrentAmount;
    public List<GoalType> goalTypes = new();

    [SerializeField]
    GameObject questTitle,goalTitle,questGoalMaxStep,questGoalDescription,questGoalProgress,completeButton,goalProgress;

    public void Awake()
    {
        // subscribes the UpdateUI function to the OnQuestUpdate event handler
        
        QuestManager.instance.OnQuestUpdateUI += UpdateQuestUI;
        foreach (Quest quest in PlayerData.instance.quests)
        {
            quest.currentGoalIndex = 0;
        }
        completeButton.SetActive(false);

    }


    private void UpdateQuestUI(object sender, EventArgs e)
    {
        if (SceneManagerScript.instance.isInWorldMap)
        {
            goalTypes.Clear();
            questCount = questRef.goals.Count;
            questCurrentStep = questRef.currentGoalIndex;
            goalAmount = currentGoalRef.amount;
            goalCurrentAmount = currentGoalRef.currentAmount;

            Goal goal = questRef.CheckCurrentGoal();
            Quest quest = questRef;


            if (goal.discriptor.type != goal.type)
            {
                Debug.LogError($"DISCRIPTOR MIS-MATCH ----------> Goal type = {goal.type} ----------> Goal Class = {goal} ----------> Goal Discriptor = {goal.discriptor}");
                goal.GenerateGoalDiscriptor(goal.type);
            }


            foreach (Goal newgoal in questRef.goals)
            {
                goalTypes.Add(newgoal.type);
            }


            if (questLogEntryIndex > PlayerData.instance.quests.Count - 1)
            {
                Debug.Log("QuestLogEntryIndex is more then the count of quests in the list.");
                // this means that the quest no longer exists in the array and should have been destroyed. 
            }
            else
            {
                if (PlayerData.instance.quests[questLogEntryIndex] == null)
                {
                    Debug.LogWarning($"Cant access index [{questLogEntryIndex}] of quest : {questRef.questName}");
                }
                else
                {
                    if (quest != null && questTitle != null)
                    {

                        questTitle.GetComponent<TMP_Text>().text = quest.questName;
                        goalTitle.GetComponent<TMP_Text>().text = goal.discriptor.goalName;
                        questGoalMaxStep.GetComponent<TMP_Text>().text = quest.goals.Count.ToString();
                        questGoalDescription.GetComponent<TMP_Text>().text = goal.discriptor.goalDescription;
                        questGoalProgress.GetComponent<TMP_Text>().text = quest.currentGoalIndex.ToString();
                        goalProgress.GetComponent<TMP_Text>().text = goal.currentAmount + " / " + goal.amount;

                    }
                    else
                    {
                        Debug.LogWarning("Goal is returning as null.");
                    }


                    if (quest.isCompleted && quest!=null)
                    {
                        questGoalProgress.GetComponent<TMP_Text>().text = questRef.goals.Count.ToString();
                        completeButton.SetActive(true);
                    }
                    else
                    {
                        completeButton.SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.Log("ISWORLDMAP is FALSE");
        }
    }

    public void CompleteButtonPressed()
    {

        
        
        if (questRef.isCompleted)
        {
            
            
            questRef.GiveReward();
            PlayerData.instance.quests.Remove(questRef);
            QuestManager.instance.questLogEntryUIs.Remove(gameObject);

            HUDManager.UpdatePartyHud();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Complete button active but the quest is not completed.");
            questRef.isCompleted = true;
            CompleteButtonPressed();
        }

        

    }
}
