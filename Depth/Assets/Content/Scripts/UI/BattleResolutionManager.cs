using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleResolutionManager : MonoBehaviour
{
    int goldReward;

    //UI

    TMP_Text expRewardText, goldRewardText;

    public void Awake()
    {
        goldRewardText = GameObject.Find("RewardContentWindow/RewardContainer/GoldText").GetComponent<TMP_Text>();
        expRewardText = GameObject.Find("RewardContentWindow/RewardContainer/ExperienceText").GetComponent<TMP_Text>();
    }

    public void Start()
    {
        CalculateWinnings();
        //StartCoroutine(StartCalulationDelay());
    }

    public void LoadWorldMap()
    {
        SceneManagerScript.LoadWorldMap();
    }

    void CalculateWinnings()
    {
        goldReward = PersistentManager.instance.enemyParty.characters.Count * Random.Range(1, 10);
        Debug.Log(goldReward);
        goldRewardText.text = "+ " + goldReward.ToString();

        PersistentManager.instance.playerParty.gold += goldReward;

        List<Character> charactersToRemove = new List<Character>();

        foreach (Character character in PersistentManager.instance.playerParty.characters)
        {
            if (character.status == CharacterStatus.Dead)
            {
                int randomNumber = Random.Range(1, 10);
                if (randomNumber > 5)
                {
                    character.status = CharacterStatus.Injured;
                }
                else
                {
                    charactersToRemove.Add(character);
                    Debug.Log(character.characterFullName + " has died.");
                }
            }

            character.currentExperience += PersistentManager.instance.enemyParty.characters.Count * Random.Range(1, 10);
            if (character.currentExperience >= character.maxExperience)
            {
                character.level += 1;
            }
        }

        // Remove dead characters after the loop
        foreach (Character character in charactersToRemove)
        {
            PersistentManager.instance.playerParty.characters.Remove(character);
        }
        RemoveNPCEnemyGameObject();


    }

    private void RemoveNPCEnemyGameObject()
    {
        Debug.LogWarning("Removing game object : " + PersistentManager.instance.npcGroup);
        PersistentManager.instance.storedNPCPartys.Remove(PersistentManager.instance.npcGroup);
        Destroy(PersistentManager.instance.enemyParty);
        Destroy(PersistentManager.instance.npcGroup);
    }


}
