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
    GameObject statCardRef;
    Transform charStatCardContent;
    GameObject statCard;
    bool statCardOpen = false;

    public Character selectedCharacter;
    public CharacterPriceList priceList;

    

    private void Start()
    {
        charStatCardContent = GameObject.FindGameObjectWithTag("CharSheetContent").transform;
        statCardRef = Resources.Load("StatCard") as GameObject;        
    }


    public void OpenCharacterCard(Character character)
    {
        selectedCharacter = character;
        if (statCardOpen == true)
        {
            Destroy(statCard);
            AddCharacterStatCard();
        }
        else
        {
            AddCharacterStatCard();
        }
    }

    
    
    //Adds stat card to UI
    public void AddCharacterStatCard()
    {
        statCard = Instantiate(statCardRef, charStatCardContent);
        statCardOpen = true;
    }
    public void RemoveCharacterStatCard()
    {
        var obj = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterMenu>().statCard;
        Destroy(obj);
        statCardOpen = false;
    }

}
