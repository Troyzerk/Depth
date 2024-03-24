using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/*
 * This Class should be used only for gamewide functions. 
 * 
 * We must eventually move all the combat functions to its own non-monobehaviour class.
 * This will make accessing it easier.
*/

public class GameState : MonoBehaviour
{

    //Global Stats 
    // Move this to a global world manager
    public float startingGameSpeed = 1f;
    public List<Faction> globalFactions;
    public Image heroImage;
    public bool isBellyFull;
    public float feedIntervalSeconds = 60f;

    private void Awake()
    {
        GroupNames.InitGroupNames();
        


        //AIGroupTracker.UpdateAIGroupTracker();
    }

    // Start is called before the first frame update
    void Start()
    {
        GlobalGameSettings.SetGameSpeed(startingGameSpeed);

        //assigning hero image.
        heroImage = HeroContainerManager.instance.heroImage.GetComponent<Image>();
        if(heroImage == null)
        {
            Debug.LogWarning("HeroImage object not found.");
        }
        else
        {
            //heroImage = PersistentManager.instance.playerCharacter.portrait;
        }
    }

    private void FixedUpdate()
    {
        if (isBellyFull == false)
        {
            isBellyFull = true;
            StartCoroutine(PartyEating());
        }
    }

    public IEnumerator PartyEating()
    {
        PlayerData.instance.playerParty.ConsumeFood(); 
        yield return new WaitForSeconds(feedIntervalSeconds);
        isBellyFull = false;
    }

    /*
     *  BEWARE!!!
     *  From here down its all auto resolve stuff. 
     *  Its currently not active but might want to use later on for an autoresolve solution.
     *  
     */

    //Returns if the attacking party won
    public bool ResolveBattle(List<Character> defendingParty, List<Character> attackingParty)
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(SimulateFight(attackingParty, defendingParty));
        //fighting is disabled for now
        return true;
    }

    //Battle Coroutines
    IEnumerator SimulateFight(List<Character> attackingParty, List<Character> defendingParty)
    {
        if (attackingParty.Count > 0)
        {
            foreach (Character attackingCharacter in attackingParty)
            {
                if (defendingParty.Count > 0)
                {
                    yield return StartCoroutine(SimulateIndividualFight(attackingCharacter, attackingParty, defendingParty));
                }
            }
        }
        else
        {

        }

    } 
    IEnumerator SimulateIndividualFight(Character attackingCharacter, List<Character> attackingParty, List<Character> defendingParty)
    {
        Character defendingCharacter = new();
        if (defendingParty.Count>0)
        {
            defendingCharacter = defendingParty[Random.Range(0, defendingParty.Count)];
        }
        print("Fight Started between : " + attackingCharacter + " and " + defendingCharacter);
        while (attackingCharacter != null || defendingCharacter != null)
        {
            DealDamage(attackingCharacter, defendingCharacter);
            if(IsCharacterDefeated(defendingCharacter, defendingParty))
            {
                yield break;
            }
            else
            {
                DealDamage(defendingCharacter, attackingCharacter);
                if (IsCharacterDefeated(attackingCharacter, attackingParty))
                {
                    yield break;
                }
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    //Additional Battle Functions
    public bool IsAttackerVictorious(List<Character> attackingParty, List<Character> defendingParty)
    {
        if (IsPartyDefeated(attackingParty))
        {
            return false;
        }
        else if (IsPartyDefeated(defendingParty))
        {
            return true;
        }
        return false;
    }
    public bool IsPartyDefeated(List<Character> party)
    {
        if (party.Count <= 0)
        {
            return true;
        }
        return false;
    }
    public bool IsCharacterDefeated(Character character, List<Character> characterParty)
    {
        if (character.currentHealth <= 0)
        {
            print(character + " is DEFEATED in battle");
            characterParty.Remove(character);
            Destroy(character);

            if (characterParty.Count <= 0)
            {
                PartyDefeated(characterParty);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public void DealDamage(Character attackingCharacter, Character defendingCharacter)
    {
        int damageDone = attackingCharacter.damage - defendingCharacter.defence;
        defendingCharacter.currentHealth = defendingCharacter.currentHealth - damageDone;
        print(attackingCharacter.characterFullName + " deals " + damageDone + " Damage to " + defendingCharacter.characterFullName);
    }
    public void PlayerPartyDefeated()
    {
        Debug.LogWarning("Player Party Defeated");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PartyDefeated(List<Character> characterList)
    {
        //Destroy(characterList.gameObect);
    }


}
