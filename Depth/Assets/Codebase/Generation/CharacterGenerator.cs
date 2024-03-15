using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterGenerator
{
    public static Character CreateNewCharacter(RaceID raceID, SubRaceID subRaceID)
    {
        Character newCharacter;
        foreach (var raceStats in PersistentManager.activeRaces)
        {
            foreach (var subRaceStats in raceStats.possibleSubRaces)
            {
                if (raceID == raceStats.raceID)
                {
                    foreach (SubRaceStats subRaceStat in raceStats.possibleSubRaces)
                    {
                        if (subRaceID == subRaceStat.subRaceID)
                        {
                            //print("Found : " + subRaceStats + " - " + raceStats);
                            newCharacter = CreateNewCharacterStats(raceStats, subRaceStat);
                            return newCharacter;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Cant confirm raceID input. " + raceID + ">>>" + subRaceID);
                }
            }

            //Debug.LogError(raceStats);
        }

        //Debug.LogError(PersistentManager.activeRaces.Count);

        Debug.LogWarning("Creation of character failed because of RaceStatCheck. This is returning null on some character creation.");
        return null;
    }
    public static Character BuyNewCharacter(RaceID raceID, SubRaceID subRaceID, Party party, int price)
    {
        Character newChar = CreateNewCharacter(raceID, subRaceID);
        if (party.gold >= price)
        {
            party.characters.Add(CreateNewCharacter(raceID, subRaceID));
            party.gold -= price;
            GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>().UpdateHUD();
        }
        return newChar;
    }

    //Sets random stats for the new character
    public static Character CreateNewCharacterStats(RaceStats raceStats, SubRaceStats subRacesStats)
    {
        //Instances the new character
        Character newCharacter = ScriptableObject.CreateInstance<Character>();

        //Setting name.
        string[] allNames = new string[2];
        if (allNames[0] == null || allNames[1] == null)
        {
            allNames = GenerateCharacterName(newCharacter, raceStats);
        }
        newCharacter.characterFullName = allNames[0] + " " + allNames[1];
        newCharacter.characterFirstName = allNames[0];
        newCharacter.characterLastName = allNames[1];

        //Setting race and sub race
        newCharacter.subRaceStats = subRacesStats;
        newCharacter.subRace = subRacesStats.subRaceID;

        //Setting of Base Stats
        newCharacter.portrait = raceStats.portraits[Random.Range(0, raceStats.portraits.Count)];
        newCharacter.damageType = raceStats.startingDamageType[Random.Range(0, raceStats.startingDamageType.Count)];
        newCharacter.speed = Random.Range(raceStats.minStartingSpeed, raceStats.maxStartingSpeed);
        newCharacter.age = Random.Range(raceStats.minStartingAge, raceStats.maxStartingAge);
        newCharacter.health = Random.Range(raceStats.minStartingHealth, raceStats.maxStartingHealth);
        newCharacter.currentHealth = newCharacter.health;
        newCharacter.mana = Random.Range(raceStats.minStartingMana, raceStats.maxStartingMana);
        newCharacter.damage = Random.Range(raceStats.minStartingDamage, raceStats.maxStartingDamage);
        newCharacter.defence = Random.Range(raceStats.minStartingDefence, raceStats.maxStartingDefence);
        newCharacter.status = CharacterStatus.Healthy;
        newCharacter.foodConsumption = Random.Range(1, 3);

        //Skills
        newCharacter.autoAttackSkill = raceStats.startingAutoAttackSkill[Random.Range(0, raceStats.startingAutoAttackSkill.Count)];

        //Adds the new character to the party and manages the party size.
        newCharacter.name = newCharacter.characterFullName;
        PersistentManager.instance.globalCharacterNames.Add(newCharacter.characterFullName);
        PersistentManager.instance.activeCharacters.Add(newCharacter);
        UpdateCharacterStatsAfterEvolution(newCharacter);
        //print("Created : " + newCharacter.race + " - " + newCharacter.subRace);
        return newCharacter;



    }
    public static void UpdateCharacterStatsAfterEvolution(Character character)
    {
        character.health += character.subRaceStats.healthModifier;
        character.currentHealth = character.health;
        character.speed += character.subRaceStats.speedModifier;
        character.damage += character.subRaceStats.damageModifier;
        character.defence += character.subRaceStats.defenceModifier;
        character.mana += character.subRaceStats.manaModifier;
        character.portrait = character.subRaceStats.portraitOverride;
    }

    /*
     * this needs to be moved into the name generator script and reworked properly.
     * we need to store the character name in a global class so that we can add and remove 
     * from it easier. 
     */

    public static string[] GenerateCharacterName(Character newCharacter, RaceStats raceStats)
    {
        for (int i = 0; i < 150; i++)
        {
            newCharacter.characterFirstName = raceStats.raceFirstNames[Random.Range(0, raceStats.raceFirstNames.Count)];
            newCharacter.characterLastName = raceStats.raceLastNames[Random.Range(0, raceStats.raceLastNames.Count)];
            string fullName = newCharacter.characterFirstName + " " + newCharacter.characterLastName;
            if (!PersistentManager.instance.globalCharacterNames.Contains(fullName))
            {
                //print(fullName + " was created.");
                string[] allNames = new string[2];
                allNames[0] = newCharacter.characterFirstName;
                allNames[1] = newCharacter.characterLastName;
                return allNames;
            }
            if (i >= 100)
            {
                Debug.LogError("GenerateNameFails on [" + i + "] itterations");
            }
        }


        Debug.LogError("Cant Generate unique name out of 150 itterations.");
        return null;

    }
}
