using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class PartyGenerator
{
    /*
     * Generate NPC party 
     */

    public static AIParty GenerateNPCParty(int size)
    {
        AIParty aiGroup = ScriptableObject.CreateInstance<AIParty>();

        aiGroup.comp = GroupComposition.Goblins;
        aiGroup.name = NameGenerator.GenerateGroupName(aiGroup);
        aiGroup.gold = Random.Range(size * 10, size * 20);
        Faction randomFaction = PersistentManager.factions[Random.Range(0,PersistentManager.factions.Length)];
        
        aiGroup.faction = randomFaction;

        if (size < 1)
        {
            Debug.Log("NPC Party can NOT have a size of less then 1");
            size = 1;
        }

        for (int i = 0; i < size; i++)
        {
            aiGroup.characters.Add(CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid));
            //aiGroup.totalDamage += newChar.damage;
            //aiGroup.totalDefence += newChar.defence;
            
        }
        Debug.Log(aiGroup.characters.Count);
        aiGroup.partySpeed = Random.Range(0.1f, 0.2f);
        aiGroup.partyLeader = aiGroup.characters.Last();
        return aiGroup;
    }

    /*
     * Generate player party 
     */

    public static PlayerParty GeneratePlayerParty(int size)
    {
        PlayerParty party = ScriptableObject.CreateInstance<PlayerParty>();

        party.partyName = NameGenerator.GenerateGroupName(party);
        party.faction = PersistentManager.factions[Random.Range(0, PersistentManager.factions.Length)];
        party.gold = 0;
        party.partySpeed = 1;
        party.reputation = 0;
        party.totalDamage = 0;
        party.totalDefence = 0;

        //generate and add characters to party
        for (int i = 0; i < size; i++)
        {
            //Debug.Log(CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid));
            party.characters.Add(CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid));
        }
        return party;
    }

}
