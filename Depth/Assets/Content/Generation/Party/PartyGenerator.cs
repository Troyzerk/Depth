using System.Collections;
using System.Collections.Generic;
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
        int partySize = size;
        Faction randomFaction = PersistentManager.factions[Random.Range(0,PersistentManager.factions.Length)];
        
        aiGroup.faction = randomFaction;

        for (int i = 0; i < partySize; i++)
        {
            Character newChar = CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid, aiGroup.characters);
            aiGroup.characters.Add(newChar);
            //aiGroup.totalDamage += newChar.damage;
            //aiGroup.totalDefence += newChar.defence;
        }

        aiGroup.partySpeed = Random.Range(0.1f, 0.2f);
        //aiGroup.partyLeader = aiGroup.characters[1];
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
            
            Character newChar = CharacterGenerator.CreateNewCharacter(RaceID.Goblin,SubRaceID.Goblinoid,party.characters);
            
            if (party.partyLeader == null)
            {
                party.partyLeader = newChar;
            }
            else
            {
                party.characters.Add(newChar);
            }
            //party.totalDamage += newChar.damage;
            //party.totalDefence += newChar.defence;
        }

        PersistentManager.instance.playerParty = party;
        PersistentManager.instance.playerCharacter = party.partyLeader;
        return party;
    }

}
