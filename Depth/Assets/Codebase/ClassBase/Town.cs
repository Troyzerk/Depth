using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Town", menuName = "Content/Create new Town")]
public class Town : ScriptableObject
{
    public string townName;
    public RaceID owningRace;
    public DamageType owningRaceDamageType;
    public string owningFamily;
    public Sprite portrait;
    public int age;
    public int defence;
    public string description;
    public bool seen = false;
    public List<BuildingType> buildings = new();

    public string[] dialogue;
    public string[] priestDialogue;

    public void Interact()
    {
        if (!seen)
        {
            DialogueSystem.instance.AddNewDialogue(dialogue, "Narrator");
            Debug.Log("Interacting with Town :" + townName);
            seen = true;
        }  
    }


    public IEnumerator ChurchHeal(Party party)
    {
        bool partyNeedsHealing = false;
        foreach (Character character in party.characters)
        {
            if (character.currentHealth< character.health)
            {
                character.currentHealth = character.health;
                DialogueSystem.instance.AddNewDialogue(priestDialogue, "Priest");

                if (character.currentHealth >= character.health)
                {
                    character.currentHealth = character.health;
                }
                else
                {
                    partyNeedsHealing = true;
                }
            }
        }

        yield return new WaitForSeconds(5f);

        if (partyNeedsHealing)
        {
            yield return ChurchHeal(party);
        }
        else
        {
            yield break;
        }
    }
}


