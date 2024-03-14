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
    public bool questsInitialised = false;
    public bool questManagerDebugs = true;



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

        foreach(Quest quest in PlayerData.instance.quests)
        {
            quest.Init();
        }
        UpdateAllQuestData();
    }

    

    public void InitQuests()
    {
        if(questsInitialised == false)
        {
            questsInitialised=true;

            if(questManagerDebugs) print("Init Quests");

            if (PlayerData.instance.quests == null)
            {
                PlayerData.instance.quests = new List<Quest>();
                PlayerData.instance.quests = GenerateQuests(3);

                foreach (Quest quest in PlayerData.instance.quests)
                {
                    AddNewQuestLogUI(quest);
                }

                for (int i = 0; i < PlayerData.instance.quests.Count; i++)
                {
                    questLogEntryUIs[i].GetComponent<QuestLogEntry>().questLogEntryIndex = i;
                }

            }
            UpdateAllQuestData();
            HUDManager.instance.UpdateHUD();
        }
    }

    public void UpdateAllQuestData()
    {
        
        OnQuestUpdateUI?.Invoke(this, EventArgs.Empty);
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
        newQuestEntry.GetComponent<QuestLogEntry>().currentGoalRef = quest.goals[quest.currentGoalIndex];
    }
    


    //------------------------------------------------------------------------------------------------------------------------------------------- Reading Activity ----------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void UpdateAllInfo(Quest quest)
    {
        quest.goals[quest.currentGoalIndex].ProgressGoal();
        quest.CheckGoalProgress();
        UpdateAllQuestData();
    }
    
    //-------------------------------------- Landmarks
    public void PickedUpLandmark(Landmark landmark)
    {
        foreach (Quest quest in PlayerData.instance.quests)
        {
            if (quest.CheckCurrentGoal() is CollectLandmarkGoal)
            {                
                UpdateAllInfo(quest);
                Debug.Log($"{quest.questName} progressed by picking up landmark");
            }
        }
    }

    //-------------------------------------- Defeat

    public void DefeatedEnemyParty(AIParty enemyParty)
    {
        foreach(var quest in PlayerData.instance.quests)
        {
            if (quest.CheckCurrentGoal() is DefeatGoal)
            {
                UpdateAllInfo(quest);
                Debug.Log($"{quest.questName} progressed by defeating enemy party");
            }
        }
    }

    public void DefeatedEnemyCharacter(Character character)
    {
        foreach(var quest in PlayerData.instance.quests)
        {
            if(quest.CheckCurrentGoal() is DefeatEnemy)
            {
                UpdateAllInfo(quest);
                Debug.Log($"{quest.questName} progressed by defeating an Enemy character");
            }
        }
    }

    //-------------------------------------- 

}
