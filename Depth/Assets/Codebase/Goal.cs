using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewGoal", menuName = "Questing/NewGoal")]
public class Goal : ScriptableObject
{
    public string goalName;
    public string description;

    public bool isCompleted;

    public int experienceReward;
    public int goldReward;
    public int characterReward;

    public virtual void Awake()
    {
        Init();
    }
    public virtual void Init()
    {

    }
    public virtual void GenerateTarget()
    {

    }
    public virtual void GivePlayerReward()
    {

    }
}
