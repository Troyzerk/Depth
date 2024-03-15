using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerParty : Party
{
    public RaceID startingRace;
    public List<Quest> activeQuests = new();

    [SerializeField]
    public float PartyFood;
    [SerializeField]
    public int TotalFoodConsumption;

    public virtual void GeneratePlayerCharacter()
    {
        PlayerData.instance.playerCharacter =  CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid);
        PersistentManager.instance.playerCharacter = PlayerData.instance.playerCharacter; // This is only temp. should be removed
        partyLeader = PlayerData.instance.playerCharacter;
    }

    public void SetFoodConsumption()
    {
        TotalFoodConsumption = 0;
        PartyFood = 100f;
        
        foreach (var character in PlayerData.instance.playerParty.characters)
        {
            TotalFoodConsumption += character.foodConsumption;
        }
        Debug.Log(TotalFoodConsumption);
    }

    public void ConsumeFood()
    {
        if (PartyFood <= 0)
        {
            //gobbos starve
            foreach (var character in PlayerData.instance.playerParty.characters)
            {
                if (character.currentHealth > character.health / 2)
                {
                    character.currentHealth = character.health / 2;
                }
            }
        }
        else
        {
            PartyFood = PartyFood - TotalFoodConsumption;
        }
    }
}