using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 *      This baseGameScript needs to be renamed to make it easier to defrinciate between the battle scene
 *      game manager and the world map game manager. 
 *      -Troy
 * 
 * 
 */

public class BattleSceneCtrl : MonoBehaviour
{
    

    public List<Character> playerMinionList;
    public List <Character> enemyMinionList;

    private Vector3 position;
    private Vector3 scaleChange;

    private GameObject clone;
    
    public GameObject _selectedObject;
    Vector3 _offSet;
    Vector3 skillOffSet;
    private Collider2D targetObject;

    public GameObject enemyTarget;
    public GameObject friendlyTarget;
    private GameObject _skillSelect;

    public GameObject tileHover;
    public GameObject fireSkill;
    public GameObject healSkill;
    public GameObject fireSkillClone;

    private Vector3 worldPosition;

    public Grid grid;

    public GameObject menu;
    public GameObject skillMenu;
    public GameObject endMenu;

    GameObject folderFound;


    public bool iSee;
    bool skillPress;

    void Start()
    {
        PersistentManager.instance.AIGroups.SetActive(false);
        PersistentManager.instance.towns.SetActive(false);
        enemyMinionList = PersistentManager.instance.enemyParty.characters;
        playerMinionList = PlayerData.instance.playerParty.characters;
        ValidatePlayerParty(playerMinionList);
        ValidateMainPlayer(PlayerData.instance.playerCharacter);

        GlobalGameSettings.SetGameSpeed(1);

        ValidateNPCParty(enemyMinionList);
        folderFound = GameObject.FindGameObjectWithTag("AI");
        folderFound.SetActive(false);
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!iSee)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

                if (targetObject != null && targetObject.gameObject.CompareTag("Minion") && skillPress == false)
                {

                    _selectedObject = targetObject.transform.gameObject;
                    _offSet = _selectedObject.transform.position - mousePosition;
                }
            }
            if (_selectedObject != null)
            {
                _selectedObject.transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
                _selectedObject.transform.position = mousePosition + _offSet;
            }

            if (Input.GetMouseButtonUp(0) && _selectedObject != null)
            {
                _selectedObject.transform.position = tileHover.transform.position;
                _selectedObject.transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
                _selectedObject = null;
            }
        }
        if (skillPress)
        {
            Vector3 skillOffSet = new Vector3(0, 0, 5);
            if (_skillSelect != null)
            {
                _skillSelect.transform.position = mousePosition + skillOffSet;
            }
            if (Input.GetMouseButtonUp(1) && _skillSelect != null)
            {
                print(_skillSelect);
                _skillSelect.transform.position = mousePosition +skillOffSet;
                _skillSelect.transform.GetChild(0).gameObject.SetActive(false);
                _skillSelect.transform.GetChild(1).gameObject.SetActive(true);
                _skillSelect.gameObject.GetComponent<Collider2D>().enabled = true;
                //_skillSelect.gameObject.GetComponent<SkillPerfab>().IsActive();
                skillPress = false;
                

            }  
        }
    }
    public void RunNet()
    {   

        menu.SetActive(false);
        iSee = true;

        GameObject[] _tilePrefabs;
        _tilePrefabs = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject Prefab in _tilePrefabs)
        {
            Destroy(Prefab);
        }
        if (iSee)
        {
            skillMenu.SetActive(true);
        }

        foreach (Character enemy in enemyMinionList)
        {
            folderFound.SetActive(true);
            if (GameObject.Find(enemy.name) != null)
            {   
                GameObject enemyObject = GameObject.Find(enemy.name);
                enemyObject.gameObject.GetComponent<MinionBrain>().FindTarget();

            }

        }

        foreach (Character player in playerMinionList)
        {
            if (GameObject.Find(player.name) != null) 
            {
                GameObject playerObject = GameObject.Find(player.name);
                playerObject.gameObject.GetComponent<MinionBrain>().FindTarget();
            }

        } 

    }
    GameObject skillPrefab;
    public void SkillCast(string skill)
    {
        GameObject skillFind = GameObject.Find ("Skills");
        //print(skillFind.transform.GetChild(1).name);
        for (int i = 0; i < skillFind.transform.childCount; i++)
        {
            if (skillFind.transform.GetChild(i).name == $"SpellCastHolder_{skill}")
            {
                skillPrefab = skillFind.transform.GetChild(i).gameObject;
            }


        }
        if (skillPrefab == null)
        {
            print($"SpellCastHolder_{skill}: If this is named differenly rename the prefab or check name in Scriptable object");
            
        }
        skillPrefab.gameObject.SetActive(true);
        _skillSelect = skillPrefab.transform.gameObject;
        skillPress = true;

    }
    public void SurrenderScene()
    {   
        Destroy(GameObject.Find("PersistentManager"));
        SceneManager.LoadScene(0);
    }

    public void Victory()
    {
        if (playerMinionList.Contains(PersistentManager.instance.playerCharacter))
        {
            playerMinionList.Remove(PersistentManager.instance.playerCharacter);
        }
        PromoteBattleParties();
        SceneManagerScript.LoadBattleResolution();
    }


    void ValidateNPCParty(List<Character> party)
    {
        
        scaleChange = new Vector3(3f, 3f, 1f);
        GameObject minionGameObject = Resources.Load("Enemy_Minion") as GameObject;
        minionGameObject.transform.localScale = scaleChange;

        for (int i = 0; i < party.Count; i++)
        {
            worldPosition = grid.GetCellCenterWorld(new Vector3Int(UnityEngine.Random.Range(8, 16), UnityEngine.Random.Range(1, 8)));
            clone = Instantiate(minionGameObject, worldPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("AI").transform);
            clone.name = party[i].name;
            clone.GetComponent<MinionBrain>().minionRef = party[i];

            GameObject weaponName = clone.GetComponent<MinionBrain>().minionRef.autoAttackSkill.weapon;
            GameObject weapon = Resources.Load(weaponName.name) as GameObject;
            Instantiate(weapon, worldPosition, Quaternion.identity, clone.transform);
        }
    }
    void ValidatePlayerParty(List<Character> party)
    {
        scaleChange = new Vector3(-3f, 3f, 1f);
        GameObject minionGameObject = Resources.Load("Minion") as GameObject;
        minionGameObject.transform.localScale = scaleChange;

        for (int i = 1; i < party.Count; i++)
        {
            
            worldPosition = grid.GetCellCenterWorld(new Vector3Int(1+i, -1));
            clone = Instantiate(minionGameObject, worldPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
            clone.name = party[i].name;
            clone.GetComponent<MinionBrain>().minionRef = party[i];

            GameObject weaponName = clone.GetComponent<MinionBrain>().minionRef.autoAttackSkill.weapon;
            GameObject weapon = Resources.Load(weaponName.name) as GameObject;
            Instantiate(weapon, worldPosition, Quaternion.identity, clone.transform);

        }
    }
    void ValidateMainPlayer(Character player)
    {
        GameObject minionGameObject = Resources.Load("mainMinon") as GameObject;
        worldPosition = grid.GetCellCenterWorld(new Vector3Int(1, -1));
        clone = Instantiate(minionGameObject, worldPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
        clone.name = player.characterFullName;
        clone.GetComponent<MinionBrain>().minionRef = player;

        GameObject weaponName = clone.GetComponent<MinionBrain>().minionRef.autoAttackSkill.weapon;
        GameObject weapon = Resources.Load(weaponName.name) as GameObject;
        Instantiate(weapon, worldPosition, Quaternion.identity, clone.transform);
    }
    public void PromoteBattleParties()
    {
        PlayerData.instance.playerParty.characters = playerMinionList;
        PersistentManager.instance.enemyParty.characters = enemyMinionList;
    }
}