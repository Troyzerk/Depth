using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyBarButtonInfoHolder : MonoBehaviour
{
    public Character character;
    public CharacterMenu characterMenu;
    public GameObject player;
    public TownMenuManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TownMenuManager>();
        characterMenu = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterMenu>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PassButtonInfo()
    {
        characterMenu.OpenCharacterCard(character);
    }

    public void CreateGoblinoidButton()
    {
        CharacterGenerator.BuyNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid, player.GetComponent<PlayerPartyManager>().playerParty,10 );

    }

    public void CreateOgreButton()
    {
        CharacterGenerator.BuyNewCharacter(RaceID.Goblin, SubRaceID.Ogre, player.GetComponent<PlayerPartyManager>().playerParty, 50);
    }
}

