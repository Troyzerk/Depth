using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used in the QuestLogManager Gameobject under the HUD
 * all UI functionality should be contained here. 
 * 
 */



public class UIQuestLogManager : MonoBehaviour
{
    public Quest activeQuest;
    public void QuestCompleted()
    {
        if (activeQuest.isCompleted)
        {
            gameObject.SetActive(false);
        }
    }
}
