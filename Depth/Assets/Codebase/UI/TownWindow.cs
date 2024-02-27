using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Attached to HUD/TownMenu GameObject

public class TownWindow : MonoBehaviour
{
    public static TownWindow instance;
    public GameObject townWindow;

    //Instanciating UI Elements
    Button exitButton, churchButton;
    TMP_Text townName, townDescription, townAge, townDefence, townOwningRace, townRaceType, townOwningFamily;
    Sprite townPortrait;

    private void Awake()
    {
        //Checking instance for the TownWindow UI
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            instance = null;
        }

        if(instance == null)
        {
            instance = this;
        }


        // define links to UI Button elements
        exitButton = townWindow.transform.Find("Exit").GetComponent<Button>();
        churchButton = townWindow.transform.Find("ChurchButton").GetComponent<Button>();

        // define links to TMP_Text elements
        townName = townWindow.transform.Find("TownName").GetComponent<TMP_Text>();
        townDescription = townWindow.transform.Find("TownDescription").GetComponent<TMP_Text>();
        townAge = townWindow.transform.Find("TownStats/TownAge").GetComponent<TMP_Text>();
        townDefence = townWindow.transform.Find("TownStats/TownDefence").GetComponent<TMP_Text>();
        townOwningRace = townWindow.transform.Find("TownStats/TownOwningRace").GetComponent<TMP_Text>();
        townRaceType = townWindow.transform.Find("TownStats/TownRaceType").GetComponent<TMP_Text>();
        townOwningFamily = townWindow.transform.Find("TownStats/TownOwningFamily").GetComponent<TMP_Text>();

        // define links to Sprite elements
        townPortrait = townWindow.transform.Find("TownPortrait").GetComponent<Sprite>();


        churchButton.onClick.AddListener(delegate { ChurchInteraction(GlobalPlayerData.selectedTown); });
        exitButton.onClick.AddListener(delegate { CloseTownWindow();});
        townWindow.SetActive(false);

        
    }

    public void OpenTownWindow()
    {
        UpdateTownWindow(GlobalPlayerData.selectedTown);
        ClickMovement.movementLock = true;
        GlobalGameSettings.SetGameSpeed(0);
        townWindow.SetActive(true);
        Debug.Log(GlobalPlayerData.selectedTown);
    }

    public void CloseTownWindow()
    {
        townWindow.SetActive(false);
        ClickMovement.movementLock = false;
        GlobalGameSettings.SetGameSpeed(1);
    }
    public void UpdateTownWindow(Town town)
    {
        townName.text = town.townName;
        townAge.text = "Age : " + town.age.ToString();
        townDescription.text = town.description;
        townDefence.text = "Defence : " + town.defence.ToString();
        townOwningRace.text = "Race : " + town.owningRace.ToString();
        townRaceType.text = "Race Type : "+town.owningRace.ToString(); // this defaults to owning race.
        townOwningFamily.text = "Family : "+town.owningFamily;

        townPortrait = town.portrait;
    }
    public void ChurchInteraction(Town town)
    {
        if (GlobalPlayerData.selectedTown.buildings.Count > 0)
        {
            if (GlobalPlayerData.selectedTown.buildings.Contains(BuildingType.Church))
            {
                StartCoroutine(town.ChurchHeal(GlobalPlayerData.playerParty));
            }
        }
        else
        {
            Debug.LogWarning(GlobalPlayerData.selectedTown.townName + " has no buildings in its class.");
        }
    }


}
