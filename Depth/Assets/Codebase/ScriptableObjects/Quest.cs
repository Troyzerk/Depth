using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Questing/NewQuest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public bool isCompleted;
    public int experienceReward;


    public void Awake()
    {
        
    }

    public void CheckGoals()
    {
        
    }

    void GiveReward()
    {
        /*
         * We will have to polish this more when the combatEvent system
         * is up and running. 
         */
    }
}


