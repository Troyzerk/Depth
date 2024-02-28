using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPartyManager : MonoBehaviour
{
    public static PlayerPartyManager instance;  
    
    public PlayerParty playerParty;
    public GameObject playerBanner;
    public Transform partyContent;
    public GameObject characterHUDItem;
    public HUDManager hudManager;

    public void Awake()
    {
        if (instance==null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        playerParty = PlayerData.instance.playerParty;
        PlayerData.instance.playerPartyObject = gameObject;

        this.gameObject.transform.position = PlayerData.instance.partyTransform; 
        
        partyContent = GameObject.FindGameObjectWithTag("PartyBarContent").transform;
        CalculateStatsTotal();

        hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        HUDManager.UpdatePartyHud();
    }

    public void CalculateStatsTotal()
    {
        UpdatePartySpeed();
        PlayerData.instance.playerParty.totalDamage = 0;
        PlayerData.instance.playerParty.totalDefence = 0;
        foreach (Character character in PlayerData.instance.playerParty.characters)
        {
            if (character != null)
            {
                PlayerData.instance.playerParty.totalDamage += character.damage;
                PlayerData.instance.playerParty.totalDefence += character.defence;
            }
            
        }
    }
    public void UpdatePartySpeed()
    {
        //hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();

        if (PlayerData.instance.playerParty.characters != null)
        {
            //Update Speed Data
            foreach (Character character in PlayerData.instance.playerParty.characters)
            {
                if (character!= null)
                {
                    PlayerData.instance.playerParty.totalSpeed = PlayerData.instance.playerParty.totalSpeed + character.speed;
                }
            }
            if (PlayerData.instance.playerParty.totalSpeed == 0)
            {
                Debug.LogWarning("Party Speed is 0 because speed isnt set.");
            }
            else
            {
                PlayerData.instance.playerParty.partySpeed = (PlayerData.instance.playerParty.totalSpeed / PlayerData.instance.playerParty.characters.Count);
                PlayerData.instance.playerParty.totalSpeed = 0;
                HUDManager.UpdatePartyHud();
            }
            
        }
        else
        {
            print("partyManager.party is empty.");
            PlayerData.instance.playerParty.totalSpeed = 0;
        }
    }
}
