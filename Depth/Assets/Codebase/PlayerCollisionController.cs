using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerCollisionController : MonoBehaviour
{
    clickColChecker clickPos;
    GameObject gameManager;
    HUDManager hudManager;
    PlayerPartyManager playerPartyManager;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        playerPartyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Town"))
        {
            if(GlobalPlayerData.selectedTown != other.gameObject.GetComponent<TownInfo>().town)
            {
                GlobalPlayerData.selectedTown = other.gameObject.GetComponent<TownInfo>().town;
            }

            GlobalPlayerData.selectedTown = other.gameObject.GetComponent<TownInfo>().town;

            // If there is a reference error here make sure TownMenu is spawned and set to active. 
            TownWindow.instance.OpenTownWindow();
            if (other.gameObject.GetComponent<TownInfo>().town.seen == false)
            {
                other.GetComponent<TownInfo>().town.Interact();
            }

        }

        if (other.gameObject.CompareTag("DungeonEntrance"))
        {
            Debug.Log("Entering Dungeon");
            SceneManagerScript.RecordStoredData();
            SceneManagerScript.LoadDungeonMap();
        }

        if (other.gameObject.CompareTag("Landmark"))
        {
            Landmark landmark = other.gameObject.GetComponent<LandmarkManager>().landmark;
            // this needs to be fully implemented when experience per character is working.
            if (landmark.rewardType == LandmarkRewardType.Experience)
            {
                Debug.Log("Player given Experience : +" + landmark.rewardAmount);
                HUDManager.instance.UpdateHUD();
            }
            if (landmark.rewardType == LandmarkRewardType.Gold)
            {
                Debug.Log("Player given Gold : +" + landmark.rewardAmount);
                PlayerData.instance.playerParty.gold += landmark.rewardAmount;
                HUDManager.instance.UpdateHUD();
            }
            if (landmark.rewardType == LandmarkRewardType.NewRecruit)
            {
                Debug.Log("Player given New Recruit");
                PlayerData.instance.playerParty.characters.Add(CharacterGenerator.CreateNewCharacter(RaceID.Goblin, SubRaceID.Goblinoid));
                HUDManager.instance.UpdateHUD();
            }
            QuestManager.instance.PickedUpLandmark(landmark);
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("AI") )
        {
            GlobalHolder.enemyPartyReference = other.GetComponent<AIBehaviour>().aiParty;
            PersistentManager.instance.enemyParty = other.GetComponent<AIBehaviour>().aiParty;
            PersistentManager.instance.npcGroup = other.gameObject;
            SceneManagerScript.RecordStoredData();
            SceneManagerScript.LoadBattleScene();

          }

        

    }
  
    public void GatherDungeonData(GameObject other)
    {
        if (other.GetComponent<DungeonEntrance>().levelData == null)
        {
            // dungeon has not been init
        }
        else
        { 
            // dungeon data found
        }
    }
}
