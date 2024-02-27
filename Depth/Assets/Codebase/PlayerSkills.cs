using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should just handle the player skills. It should pull from a list of existing skills and add if required

public class PlayerSkills : MonoBehaviour
{
    public List<WorldSkill> playerSkills;
    public GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");

        if (gameManager == null)
        {
            Debug.LogError("Game manager object not found with the specified tag!");
        }
    }

    public void AddSkillToPlayer(WorldSkill newSkill)
    {
        playerSkills.Add(newSkill);
        newSkill.Activate();
    }

    public void ClearPlayerSkills()
    {
        foreach (WorldSkill skill in playerSkills)
        {
            skill.Deactivate();
        }
    }
}
