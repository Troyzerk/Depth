using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public PlayerPartyManager playerPartyManager;
    public Town town;


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
        partyContent = GameObject.FindGameObjectWithTag("PartyBarContent").transform;
        UpdateHUD();
    }
    public void Init()
    {
        playerPartyManager = GameObject.Find("Player").GetComponent<PlayerPartyManager>();
        goldCounter = GameObject.Find("GoldCounterText").GetComponent<TMP_Text>();
        repCounter = GameObject.Find("RepCounterText").GetComponent<TMP_Text>();
        partySpeedCounter = GameObject.Find("PartySpeedCounterText").GetComponent<TMP_Text>();
        partyDefenceCounter = GameObject.Find("PartyDefence").GetComponent<TMP_Text>();
        partyDamageCounter = GameObject.Find("PartyDamage").GetComponent<TMP_Text>();

    }

    public void UpdateHUD()
    {
        //Init();
        playerPartyManager = GameObject.Find("Player").GetComponent<PlayerPartyManager>();
        playerPartyManager.CalculateStatsTotal();
        goldCounter.text = PersistentManager.instance.playerParty.gold.ToString();
        repCounter.text = PersistentManager.instance.playerParty.reputation.ToString();
        float simpleSpeed = Mathf.Round(PersistentManager.instance.playerParty.partySpeed +100)/100;
        partySpeedCounter.text = simpleSpeed.ToString();
        partyDefenceCounter.text = PersistentManager.instance.playerParty.totalDefence.ToString();
        partyDamageCounter.text = PersistentManager.instance.playerParty.totalDamage.ToString();
        UpdatePartyHud();
        print("Updated entire HUD");
    }


    public static void UpdatePartyHud()
    {
        //Update Party size UI on banner
        GameObject.Find("Player/Banner/BannerPartyCountText").GetComponent<TextMesh>().text = PersistentManager.instance.playerParty.characters.Count.ToString();

        //Cleanup of HUD before Updating
        foreach (GameObject obj in partyHud)
        {
            Destroy(obj);
        }
        partyHud.Clear();

        //Updating Party Hud to show characters
        foreach (Character character in PersistentManager.instance.playerParty.characters)
        {
            if (character!=null)
            {
                Transform partyContent = GameObject.FindGameObjectWithTag("PartyBarContent").transform;
                GameObject obj = Instantiate(Resources.Load("characterPortraitButton") as GameObject, partyContent);
                obj.name = character.name;

                if (!partyHud.Contains(obj))
                {
                    //Sets all Button Info and adds a referece to the character//
                    partyHud.Add(obj);
                    var partyBarButton = obj.transform.GetComponent<PartyBarButtonInfoHolder>();
                    var charName = obj.transform.Find("Frame").Find("CharacterName").GetComponent<TMP_Text>();
                    var charPortrait = obj.transform.Find("Frame").Find("Portrait").GetComponent<Image>();

                    partyBarButton.character = character;
                    charPortrait.sprite = character.portrait;
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
