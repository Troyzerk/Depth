using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script should handle all skill types and classes when for the game. 
//We should thing about potentially moving this into seperate scripts for MoveSkill/AttackSkill etc.

public abstract class WorldSkill : ScriptableObject
{
    public string skillName;
    public string skillDisplayName;
    public float skillLevel;
    public int skillMaxExp;
    public int skillCurrentExp;
    public bool skillMaxed;

    public abstract void Activate();
    public abstract void Run();
    public abstract void Deactivate();
}

//  MOVE SKILLS //  
[CreateAssetMenu(fileName = "NewMoveSkill", menuName = "MoveSkills/Standard")]
public class MoveSkill : WorldSkill
{
    public float moveSpeedIncreasePerLevel = 1f;
    private PlayerStatController playerStats;

    public MoveSkill()
    {
        skillName = "Move";
        skillLevel = 1;
        skillDisplayName = skillName +" "+ skillLevel;
        skillMaxExp = 100;
        skillCurrentExp = 0;
        skillMaxed = false;
    }
    public override void Run()
    {
        //on running the Skill
    }

    public override void Activate()
    {
        // Do functionality on activating a skill

        playerStats = GameObject.FindWithTag("PlayerController").GetComponent<PlayerStatController>();
        playerStats.baseSpeed = playerStats.baseSpeed + moveSpeedIncreasePerLevel;
        Debug.Log("Crawl " + skillLevel +": skill activated");
    }
    public override void Deactivate()
    {
        //when you deactivate skill
    }
}




