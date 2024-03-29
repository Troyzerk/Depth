using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 *  After some investigation we should be using this HUDManager to kick off functions and assign subscriptions to 
 *  trigger events for global updates. We should try to keep individual subscriptions isolated to HUD scripts.
 *  
 *  For example : 
 *      We should have the the HeroStatCard subscribed to the Character stat change event so that when stats change the card
 *      triggers its UI update. 
 *  
 *  This should be the case for all UI Updating in this project. 
 *  
 *  -Troy 01/03/24
 * 
 */


public class HUDManager : MonoBehaviour
{
    public Town town;
    public static HUDManager instance;


    // UI Refs
    public TMP_Text goldCounter;
    public TMP_Text repCounter;
    public TMP_Text partySpeedCounter;
    public TMP_Text partyDefenceCounter;
    public TMP_Text partyDamageCounter;

    //party hud ref
    public Transform partyContent;
    public GameObject characterHUDItem;
    public static List<GameObject> partyHud = new();

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        partyContent = GameObject.FindGameObjectWithTag("PartyBarContent").transform;
        
    }
    public void Init()
    {
        goldCounter = HUDGlobalStats.instance.GoldCounterText.GetComponent<TMP_Text>();
        repCounter = HUDGlobalStats.instance.RepCounterText.GetComponent<TMP_Text>();
        partySpeedCounter = HUDGlobalStats.instance.PartySpeedCounterText.GetComponent<TMP_Text>();
        partyDefenceCounter = HUDGlobalStats.instance.PartyDefenceCounterText.GetComponent<TMP_Text>();
        partyDamageCounter = HUDGlobalStats.instance.PartyDamageCounterText.GetComponent<TMP_Text>();
        HeroContainerManager.instance.StatButtonPress();
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        PlayerPartyManager.instance.CalculateStatsTotal();
        goldCounter.text = PlayerData.instance.playerParty.gold.ToString();
        repCounter.text = PlayerData.instance.playerParty.reputation.ToString();
        float simpleSpeed = Mathf.Round(PlayerData.instance.playerParty.partySpeed +100)/100;
        partySpeedCounter.text = simpleSpeed.ToString();
        partyDefenceCounter.text = PlayerData.instance.playerParty.totalDefence.ToString();
        partyDamageCounter.text = PlayerData.instance.playerParty.totalDamage.ToString();
        UpdatePartyHud();
    }


    public static void UpdatePartyHud()
    {
        //Update Party size UI on banner
        GameObject.Find("Player/Banner/BannerPartyCountText").GetComponent<TextMesh>().text = PlayerData.instance.playerParty.characters.Count.ToString();

        //Cleanup of HUD before Updating
        foreach (GameObject obj in partyHud)
        {
            Destroy(obj);
        }
        partyHud.Clear();

        //Updating Party Hud to show characters
        foreach (Character character in PlayerData.instance.playerParty.characters)
        {
            if (character!=null)
            {
                Transform partyContent = GameObject.FindGameObjectWithTag("PartyBarContent").transform;
                GameObject obj = Instantiate(Resources.Load("UI/characterPortraitButton") as GameObject, partyContent);
                obj.name = character.name;

                if (!partyHud.Contains(obj))
                {
                    //Sets all Button Info and adds a referece to the character//
                    partyHud.Add(obj);
                    var partyBarButton = obj.transform.GetComponent<PartyBarButtonInfoHolder>();
                    var charName = obj.transform.Find("Frame").Find("CharacterName").GetComponent<TMP_Text>();
                    var charPortrait = obj.transform.Find("Frame").Find("Portrait").GetComponent<Image>();

                    partyBarButton.character = character;

                    //This needs to be re-enabled but it causes a null ref error.
                    //charPortrait.sprite = character.portrait.sprite;
                    charName.text = character.characterFullName;

                }
                else
                {
                    Destroy(obj);
                }
                obj.name = character.name;
            }
            
        }
    }
}
