using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TMPro.Examples;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/* This script enables and disables the character stat card when you click on a character portrait
 * This should probably be moved into a UI script inside of the HUD/CharacterStatCard gameobject
 * 
 */


public class CharacterMenu : MonoBehaviour
{

    GameObject statCard;

    public Character selectedCharacter;
    public CharacterPriceList priceList;

    

    private void Start()
    {
        statCard = GameObject.Find("HUD/CharacterStatCard/Content/StatCard");
        if (statCard == null)
        {
            Debug.LogWarning("Character Stat card not found");
        }
        statCard.SetActive(false);
    }


    public void OpenCharacterCard(Character character)
    {
        selectedCharacter = character;
        statCard.GetComponent<StatCardController>().EnableWindow(character);
    }


}
