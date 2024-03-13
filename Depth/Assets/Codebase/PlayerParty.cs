using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerParty : Party
{
    public RaceID startingRace;
    public List<Quest> activeQuests = new();
    public float PartyFood;
    public int ConsumptionRate;

    public virtual void GeneratePlayerCharacter()
    {
        PlayerData.instance.playerCharacter =  CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid);
        PersistentManager.instance.playerCharacter = PlayerData.instance.playerCharacter; // This is only temp. should be removed
        partyLeader = PlayerData.instance.playerCharacter;
    }



    private void FixedUpdate()
    {
        ConsumptionRate =

        //PartyFood - ConsumptionRate * Time.deltaTime;
    }
}