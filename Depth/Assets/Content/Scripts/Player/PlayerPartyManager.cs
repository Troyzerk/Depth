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
            PersistentManager.instance.playerPartyObject = this.gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        playerParty = PersistentManager.instance.playerParty;

        if(PersistentManager.instance.storedPlayerTransform != null)
        {
            this.gameObject.transform.position = PersistentManager.instance.storedPlayerTransform;
        }
        
        partyContent = GameObject.FindGameObjectWithTag("PartyBarContent").transform;
        CalculateStatsTotal();

        hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        HUDManager.UpdatePartyHud();
    }

    public void CalculateStatsTotal()
    {
        UpdatePartySpeed();
        PersistentManager.instance.playerParty.totalDamage = 0;
        PersistentManager.instance.playerParty.totalDefence = 0;
        foreach (Character character in PersistentManager.instance.playerParty.characters)
        {
            if (character != null)
            {
                PersistentManager.instance.playerParty.totalDamage += character.damage;
                PersistentManager.instance.playerParty.totalDefence += character.defence;
            }
            
        }
    }
    public void UpdatePartySpeed()
    {
        //hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();

        if (PersistentManager.instance.playerParty.characters != null)
        {
            //Update Speed Data
            foreach (Character character in PersistentManager.instance.playerParty.characters)
            {
                if (character!= null)
                {
                    PersistentManager.instance.playerParty.totalSpeed = PersistentManager.instance.playerParty.totalSpeed + character.speed;
                }
            }
            if (PersistentManager.instance.playerParty.totalSpeed == 0)
            {
                Debug.LogWarning("Party Speed is 0 because speed isnt set.");
            }
            else
            {
                PersistentManager.instance.playerParty.partySpeed = (PersistentManager.instance.playerParty.totalSpeed / PersistentManager.instance.playerParty.characters.Count);
                PersistentManager.instance.playerParty.totalSpeed = 0;
                HUDManager.UpdatePartyHud();
            }
            
        }
        else
        {
            print("partyManager.party is empty.");
            PersistentManager.instance.playerParty.totalSpeed = 0;
        }
    }
}
