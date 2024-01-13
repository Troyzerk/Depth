using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> currentQuests = new();
    public QuestGoal currentGoal;
    public List<GameObject> questEntryUIs = new();

    // This starting quest gets set all the time. We will need to somehow make this dynamic
    public Quest startingQuest;
    // Add methods for starting quests, completing quests, updating objectives, etc.

    public void Start()
    {
        currentQuests.Clear();
        currentQuests.Add(QuestGenerator.GenerateQuest("first Quest", "kill someone", QuestGoalType.Kill,1));
        startingQuest.currentGoal = startingQuest.questGoals[0];
        currentGoal = startingQuest.currentGoal;
        ReinitializeQuestEntrys();
    }

    public void GenerateRandomQuest()
    {

    }

    public void ReinitializeQuestEntrys()
    {
        /*
         * Currently we do not check if the quest already exists as an entry so we will have to be carful when adding
         * new quests as entrys.
         * 
         * 
         */
        
        
        
        foreach (GameObject obj in questEntryUIs)
        {
            Destroy(obj);
        }
        GameObject QuestLog = GameObject.Find("HUD/QuestLog");
        foreach (Quest quest in currentQuests)
        {
            GameObject obj = Instantiate(Resources.Load("QuestEntry") as GameObject, QuestLog.transform);
            obj.name = "QuestEntry-" + quest.questName;
            GameObject.Find("HUD/QuestLog/" + obj.name + "/Title/QuestTitle").GetComponent<TMP_Text>().text = quest.questName;
            GameObject.Find("HUD/QuestLog/" + obj.name + "/Title/QuestGoalStep").GetComponent<TMP_Text>().text = "1/"+ quest.questGoals.Count;
            GameObject.Find("HUD/QuestLog/" + obj.name + "/QuestGoalDescription").GetComponent<TMP_Text>().text = quest.currentGoal.description;
            GameObject.Find("HUD/QuestLog/" + obj.name + "/QuestGoalProgress").GetComponent<TMP_Text>().text = quest.currentGoal.currentAmount.ToString() + "/" + startingQuest.currentGoal.requiredAmount.ToString();
        }
    }
    
    
    // We should use this as a quick update rather then always destroying and recreating
    public void UpdateExistingQuestEntrys()
    {
        foreach(GameObject questEntry in questEntryUIs)
        {
            //GameObject.Find("HUD/QuestLog/" + questEntry.name + "/Title/QuestTitle").GetComponent<TMP_Text>().text = quest.questName;
        }
    }
}
