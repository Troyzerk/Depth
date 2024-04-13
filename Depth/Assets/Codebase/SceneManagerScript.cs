using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance { get; private set; }
    public Scene scene;
    public bool isInWorldMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance = this;
        }
        scene = SceneManager.GetActiveScene();
        isInWorldMap = true;
    }

    public static void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle_Scene");
        SwitchActiveStates(false);
        instance.isInWorldMap = false;
    }

    public static void LoadBattleResolution()
    {
        SceneManager.LoadSceneAsync("BattleResolution");
        instance.isInWorldMap = false;
    }

    public static void LoadWorldMap()
    {
        SceneManager.LoadScene("LevelTest");
        SwitchActiveStates(true);
        instance.isInWorldMap = true;
        QuestManager.instance.UpdateAllQuestData();
        PlayerData.instance.playerPartyObject.GetComponent<ClickMovement>().Reload();
    }

    public static void RecordStoredData()
    {
        GameObject[] towns = GameObject.FindGameObjectsWithTag("Town");
        foreach (GameObject town in towns)
        {
            if (!PersistentManager.instance.storedTowns.Contains(town))
            {
                PersistentManager.instance.storedTowns.Add(town);
            }
        }

        GameObject[] landmarks = GameObject.FindGameObjectsWithTag("Landmark");
        foreach (GameObject landmark in landmarks)
        {
            if (!PersistentManager.instance.storedTowns.Contains(landmark))
            {
                PersistentManager.instance.storedTowns.Add(landmark);
            }
        }


        GameObject[] aiGroups = GameObject.FindGameObjectsWithTag("AI");
        foreach (GameObject npcGroup in aiGroups)
        {
            if (!PersistentManager.instance.storedNPCPartys.Contains(npcGroup))
            {
                PersistentManager.instance.storedNPCPartys.Add(npcGroup);
            }

        }
        if (PersistentManager.instance.AIGroups == null && PersistentManager.instance.towns == null)
        {
            PersistentManager.instance.AIGroups = GameObject.Find("AIGroups");
            PersistentManager.instance.towns = GameObject.Find("Towns");
            PersistentManager.instance.landmarks = GameObject.Find("Landmarks");
        }

        PlayerData.instance.partyTransform = GameObject.FindGameObjectWithTag("Player").transform.position;
        PersistentManager.instance.PrintRecordedData();
    }

    public static void SwitchActiveStates(bool isActive)
    {
        PersistentManager.instance.AIGroups.SetActive(isActive);
        PersistentManager.instance.towns.SetActive(isActive);
        PersistentManager.instance.landmarks.SetActive(isActive);
        HUDManager.instance.gameObject.SetActive(isActive);
        PlayerData.instance.playerPartyObject.SetActive(isActive);
    }
}