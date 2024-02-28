using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * This script will manage the triggering of the all Quest functionality 
 * 
 */

public class QuestManager : MonoBehaviour
{
    

    public void Start()
    {
        
        if (PersistentManager.instance.playerQuests == null)
        {
            Debug.LogWarning("Quest Manager is empty");
        }
    }

    public void UpdateExistingQuestEntrys()
    {
        foreach(Quest quest in PersistentManager.instance.playerQuests)
        {
            //GameObject.Find("HUD/QuestLog/" + questEntry.name + "/Title/QuestTitle").GetComponent<TMP_Text>().text = quest.questName;
        }
    }

    public List<Quest> CheckAllQuests()
    {
        List<Quest> quests = new List<Quest>();

        foreach (Quest quest in PersistentManager.instance.playerQuests)
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
}
