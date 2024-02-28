using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.CoreUtils;

/*
 * This script will manage the triggering of the all Quest functionality 
 * 
 */

public class QuestManager : MonoBehaviour
{

    public event EventHandler OnQuestUpdate;
    public event EventHandler OnQuestComplete;
    public event EventHandler OnQuestFailed;
    private string questResourceDir = "Quests";


    public void Start()
    {
        
        if (PlayerData.instance.quests == null)
        {
            Debug.LogWarning("Quest Manager is empty");
        }
        else
        {
            print("init quests");
            InitQuests();
        }
    }



    public void UpdateQuests()
    {
        foreach(Quest quest in PlayerData.instance.quests)
        {

        }

        OnQuestUpdate?.Invoke(this, EventArgs.Empty);
    }

    public void InitQuests()
    {
        if (PlayerData.instance.quests.Count <= 0)
        {
            PlayerData.instance.quests.Clear();
            PlayerData.instance.quests = new List<Quest>();
            PlayerData.instance.quests = ImportRandomQuestFromResources();
        }
        
        OnQuestUpdate?.Invoke(this, EventArgs.Empty);
    }

    public List<Quest> CheckAllQuests()
    {
        List<Quest> quests = new List<Quest>();

        foreach (Quest quest in PlayerData.instance.quests)
        {
            if (quest.isCompleted)
            {

            }
            else
            {

            }
        }

        return quests;
    }

    private List<Quest> ImportRandomQuestFromResources()
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
            questList.AddRange(questArray);
            print("Imported Random Quest because persistant manager doesnt have any set.");
            return questList;
        }

        
    }
}
