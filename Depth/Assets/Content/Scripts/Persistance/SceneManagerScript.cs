using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance { get; private set; }
    public Scene scene;

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
    }

    public static void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle_Scene");
        PersistentManager.instance.AIGroups.SetActive(false);
        PersistentManager.instance.towns.SetActive(false);
        PersistentManager.instance.landmarks.SetActive(false);
    }

    public static void LoadBattleResolution()
    {
        SceneManager.LoadSceneAsync("BattleResolution");
    }

    public static void LoadWorldMap()
    {
        SceneManager.LoadScene("LevelTest");
        PersistentManager.instance.AIGroups.SetActive(true);
        PersistentManager.instance.towns.SetActive(true);
        PersistentManager.instance.landmarks.SetActive(true);


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
        
        PersistentManager.instance.storedPlayerTransform =  GameObject.FindGameObjectWithTag("Player").transform.position;
        PersistentManager.instance.PrintRecordedData();
    }
}
