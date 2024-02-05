using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseGameScript : MonoBehaviour
{

    public List<Character> playerMinionList;
    public List <Character> enemyMinionList;

    private Vector3 position;
    private Vector3 scaleChange;

    private GameObject clone; 
    
    public GameObject _selectedObject;
    public Vector3 _offSet;
    private Collider2D targetObject;

    public GameObject tileHover;
    public GameObject fireSkill;
    public GameObject fireSkillClone;

    private Vector3 worldPosition;

    public Grid grid;

    public GameObject menu;
    public GameObject skillMenu;
    public GameObject endMenu;

    private bool isShowing;

    private string tileName;
    private GameObject _tilePref;
    private int width, height;

    public bool iSee;
    bool skillPress;

    void Start()
    {
        PersistentManager.instance.AIGroups.SetActive(false);
        PersistentManager.instance.towns.SetActive(false);
        enemyMinionList = PersistentManager.instance.enemyParty.characters;
        playerMinionList = PersistentManager.instance.playerParty.characters;
        ValidatePlayerParty(playerMinionList);
    }
    void Update()
    {   
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject= Physics2D.OverlapPoint(mousePosition);
            if (targetObject != null && targetObject.gameObject.CompareTag("Minion"))
            {
                _selectedObject = targetObject.transform.gameObject;
                
                _offSet = _selectedObject.transform.position - mousePosition;
            }
            

        }
        if (_selectedObject != null)
        {   
            
            _selectedObject.transform.position = mousePosition + _offSet;
        }
        
        if (Input.GetMouseButtonUp(0) && _selectedObject != null )
        {
            _selectedObject.transform.position = tileHover.transform.position;
            _selectedObject = null;
        }
        if (skillPress)
        {
            _selectedObject = fireSkillClone.transform.gameObject;
            _offSet = _selectedObject.transform.position - mousePosition;

            if (_selectedObject != null)
            {
                _selectedObject.transform.position = mousePosition + _offSet;
            }
            if (Input.GetMouseButtonUp(1) && _selectedObject !=null)
            {
                print("upMouse");
                fireSkillClone.transform.position = mousePosition+ _offSet;
                skillPress = false;
                _selectedObject = null;
            }
        }
    }
    public void RunNet()
    {   
        width = 14;
        height = 8;

        menu.SetActive(false);
        iSee = true;

        ValidateNPCParty(enemyMinionList);

        for (int x = 0; x<width;x++)
        {
            for (int y = 0; y<height;y++)
            {
                tileName = $"Tile{x}{y}";
                _tilePref = GameObject.Find(tileName);
                Destroy(_tilePref);
            }
        }
        if (iSee)
        {
            skillMenu.SetActive(true);
        }

    }
    public void SkillFire()
    {
        print("Fire Skill Set");
        skillPress = true;
        Vector3 pos = new Vector3(0, 0, 0);
        fireSkillClone = Instantiate(fireSkill);
        fireSkillClone.transform.position = pos;

    }
    public void SurrenderScene()
    {   
        Destroy(GameObject.Find("PersistentManager"));
        SceneManager.LoadScene(0);
    }

    public void Victory()
    {
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
            worldPosition = grid.GetCellCenterWorld(new Vector3Int(UnityEngine.Random.Range(8, 14), UnityEngine.Random.Range(1, 8)));
            clone = Instantiate(minionGameObject, worldPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("AI").transform);
            clone.name = party[i].name;
            clone.GetComponent<MinionBrain>().minionRef = party[i];
        }
    }
    void ValidatePlayerParty(List<Character> party)
    {
        scaleChange = new Vector3(-3f, 3f, 1f);
        GameObject minionGameObject = Resources.Load("Minion") as GameObject;
        minionGameObject.transform.localScale = scaleChange;

        for (int i = 0; i < party.Count; i++)
        {
            worldPosition = grid.GetCellCenterWorld(new Vector3Int(-2, i));
            clone = Instantiate(minionGameObject, worldPosition, Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
            clone.name = party[i].name;
            clone.GetComponent<MinionBrain>().minionRef = party[i];

        }
    }
    public void PromoteBattleParties()
    {
        PersistentManager.instance.playerParty.characters = playerMinionList;
        PersistentManager.instance.enemyParty.characters = enemyMinionList;
    }

}
