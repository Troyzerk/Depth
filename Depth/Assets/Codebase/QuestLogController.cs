using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLogController : MonoBehaviour
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
