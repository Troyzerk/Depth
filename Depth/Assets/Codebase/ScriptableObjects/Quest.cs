using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Questing/NewQuest")]
public class Quest : ScriptableObject
{
    public List<Goal> goals;
    public int currentGoalIndex;


    public string questName;
    public string description;


    public int questProgressCurrentStep, experienceReward, goldReward, characterReward;

    public bool isCompleted;


    void Awake()
    {
        
    }

    public virtual void CheckGoals()
    {
        
    }

    public virtual void GiveReward()
    {
        /*
         * We will have to polish this more when the combatEvent system
         * is up and running. 
         */
    }
}


