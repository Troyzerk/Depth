using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.CoreUtils;

/*
 * This script will manage the triggering of the all Quest functionality and control the EventHandlers for updating UI
 * This script should be attached to the PersistantManager GameObject
 * 
 */

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get;set;}
    public GameObject questLog;

    public event EventHandler OnBattleResolved;


    public event EventHandler OnQuestUpdateUI;

    private List<string> questNames = new();
    
    public List<GameObject> questLogEntryUIs = new();



    public void Init()
    {
        questNames.Add("Red Quest");
        questNames.Add("Blue Quest");
        questNames.Add("Green Quest");
        questNames.Add("White Quest");
        questNames.Add("Black Quest");
        questNames.Add("Orange Quest");
        
        
        questLog = GameObject.FindGameObjectWithTag("QuestLog");
        InitQuests();
    }

    

    public void InitQuests()
    {
        print("Init Quests");
        if (PlayerData.instance.quests == null)
        {
            PlayerData.instance.quests = new List<Quest>();
            PlayerData.instance.quests = GenerateQuests(3);

            

            print(PlayerData.instance.quests[0].goals[0].discriptor);
            foreach (Quest quest in PlayerData.instance.quests)
            {
                AddNewQuestLogUI(quest);
            }
            
            print(PlayerData.instance.quests[0].goals[0].discriptor);
            for (int i = 0; i < PlayerData.instance.quests.Count; i++)
            {
                questLogEntryUIs[i].GetComponent<QuestLogEntry>().questLogEntryIndex = i;
            }
            
        }
        Debug.LogWarning(PlayerData.instance.quests[1].currentGoalIndex);
        UpdateAllQuestData();
    }

    public void UpdateAllQuestData()
    {
        
        ReassignQuestIndex();
        OnQuestUpdateUI?.Invoke(this, EventArgs.Empty);

    }
    
    public void CompletedCurrrentGoal(Quest quest)
    {
        quest.CheckCurrentGoal().isCompleted = true;
        quest.ProgressQuest();
        Debug.Log(quest.questName + " : Completed! -> Updating UI");
        UpdateAllQuestData();
    }

    Quest GenerateQuest(int numOfGoals)
    {
        Quest quest = new Quest();
        quest.goldReward = 100;
        quest.goldReward = 100;
        quest.questName = questNames[UnityEngine.Random.Range(0, questNames.Count)];
        quest.description = questNames[UnityEngine.Random.Range(0, questNames.Count)];
        quest.currentGoalIndex = 0;
        quest.PopulateQuestWithRandomGoals(numOfGoals);
        return quest;
    }
    List<Quest> GenerateQuests(int numberOfQuests)
    {
        List<Quest> quests = new List<Quest>();
        for(int i = 0; i < numberOfQuests; i++)
        {
            quests.Add(GenerateQuest(UnityEngine.Random.Range(1, 3)));
        }
        return quests;
    }
    private void AddNewQuestLogUI(Quest quest)
    {
        GameObject newQuestEntry = Instantiate(Resources.Load("QuestEntry") as GameObject, questLog.transform);
        questLogEntryUIs.Add(newQuestEntry);
        newQuestEntry.GetComponent<QuestLogEntry>().questRef = quest;
    }

    public void DefeatedEnemyParty(AIParty enemyParty)
    {
        foreach(Quest quest in PlayerData.instance.quests)
        {
            if(quest.CheckCurrentGoal() is DefeatGoal)
            {
                CompletedCurrrentGoal(quest);
            }
        }
        UpdateAllQuestData();
        print("Resolving quests finished");
    }
    

    public void ReassignQuestIndex()
    {
        for (int i = 0;i < PlayerData.instance.quests.Count; i++)
        {
            PlayerData.instance.quests[i].currentGoalIndex = i;
        }
    }


    //------------------------------------------------------------------------------------------------------------------------------------------- Reading Activity ----------------------------------------------------------------------------------------------------------------------------------------------------------------
    //-------------------------------------- Landmarks
    public void PickedUpLandmark(Landmark landmark)
    {
        foreach (Quest quest in PlayerData.instance.quests)
        {
            if (quest.CheckCurrentGoal() is CollectLandmarkGoal && landmark.rewardType == LandmarkRewardType.Gold)
            {
                CompletedCurrrentGoal(quest);
                Debug.Log($"{quest.questName} progressed by picking up gold landmark");
            }
            else if (landmark.rewardType == LandmarkRewardType.Experience)
            {

            }
            else if (landmark.rewardType == LandmarkRewardType.NewRecruit)
            {

            }
        }
    }

    //-------------------------------------- 


}
