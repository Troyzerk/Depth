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
        clickPos = GameObject.Find("ClickCol").GetComponent<clickColChecker>();
        hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        playerPartyManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Town") && clickPos.check == true)
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
        if (other.gameObject.CompareTag("AI") )
        {
            List<Character> playerParty = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPartyManager>().playerParty.characters;
            AIParty aiParty = other.GetComponent<AIBehaviour>().aiParty;
            print("Player VS " + aiParty.partyName );
            int fullReward = aiParty.characters.Count * 10;
            int targetPartyCount = aiParty.characters.Count;

            GlobalHolder.enemyPartyReference = aiParty;

            PersistentManager.instance.enemyParty = aiParty;
            PersistentManager.instance.npcGroup = other.gameObject;
            SceneManagerScript.RecordStoredData();
            SceneManager.LoadScene("Battle_Scene");

            //Temp removed to test character battles

            
            /*
            bool victorious = gameManager.GetComponent<GameState>().ResolveBattle(aiParty.characters, playerParty);

            if (victorious)
            {
                Destroy(other.gameObject);
                playerPartyManager.playerParty.gold += fullReward;
                hudManager.UpdateHUD();
            }
            else if (!victorious && playerParty.Count != 0)
            {
                //unresolved
                targetPartyCount -= aiParty.characters.Count;
                playerPartyManager.playerParty.gold += targetPartyCount * 10;
            }
            else if (!victorious && playerParty.Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            */
            
          }

    }


}
