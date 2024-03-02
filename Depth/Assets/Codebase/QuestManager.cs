using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public static QuestManager instance { get;set; }
    public GameObject questLog;

    public event EventHandler OnBattleResolved;


    public event EventHandler OnQuestUpdateUI;
    
    public List<GameObject> questLogEntryUIs = new List<GameObject>();
    private string questResourceDir = "Quests";



    public void Init()
    {
        questLog = GameObject.FindGameObjectWithTag("QuestLog");
        if (PlayerData.instance.quests == null)
        {
            Debug.LogWarning("Quest Manager is empty");
        }
        else
        {
            InitQuests();
        }
    }

    

    public void InitQuests()
    {
        if (PlayerData.instance.quests.Count <= 0)
        {
            PlayerData.instance.quests.Clear();
            PlayerData.instance.quests = new List<Quest>();
            PlayerData.instance.quests = ImportRandomQuestFromResources(1);

            foreach (Quest quest in PlayerData.instance.quests)
            {
                AddNewQuestLogUI();
            }

            for (int i = 0; i < PlayerData.instance.quests.Count; i++)
            {
                questLogEntryUIs[i].GetComponent<QuestLogEntry>().questLogEntryIndex = i;
            }

        }
        UpdateAllQuestData();
    }

    public void UpdateAllQuestData()
    {
        foreach (Quest quest in PlayerData.instance.quests)
        {
            if (quest.currentGoalIndex >= quest.goals.Count)
            {
                quest.isCompleted = true;
            }
        }
        OnQuestUpdateUI?.Invoke(this, EventArgs.Empty);
        
    }
    

    private List<Quest> ImportRandomQuestFromResources(int amount)
    {
        Quest[] questArray = Resources.LoadAll("Quests", typeof(Quest)).Cast<Quest>().ToArray();

        if (questArray == null)
        {
            // if your getting an error here please check the resource folder that contains quests.
            // you should have scriptable objects of type Quest in there. 
            Debug.LogWarning("Could not import any quests for dir: Resources/" + questResourceDir);
            return null;
        }
        else
        {
            List<Quest> questList = new();
            List<Quest> newQuestList = new();
            questList.AddRange(questArray);
            for (int i = 0;i < amount;)
            {
                newQuestList.Add(questList[UnityEngine.Random.Range(0, questList.Count)]);
                i++;
            }
            Debug.LogWarning("Imported Random Quest because persistant manager doesnt have any set.");
            return questList;
        }

        
    }
    private void AddNewQuestLogUI()
    {
        GameObject newQuestEntry = Instantiate(Resources.Load("QuestEntry") as GameObject, questLog.transform);
        questLogEntryUIs.Add(newQuestEntry);
    }

    public void DefeatedEnemyParty(AIParty enemyParty)
    {
        foreach(Quest quest in PlayerData.instance.quests)
        {
            if(quest.CheckCurrentGoal() is DefeatGoal)
            {
                quest.CheckCurrentGoal().isCompleted = true;
                Debug.Log(quest.questName + " : Completed! -> Updating UI");
                UpdateAllQuestData();
            }
        }
    }







}
