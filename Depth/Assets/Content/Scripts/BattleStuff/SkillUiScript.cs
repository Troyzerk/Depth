using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class SkillUiScript : MonoBehaviour
{
    [SerializeField] private BaseGameScript _battleSceneCtrl;

    [SerializeField] private Button fireSkill;
    [SerializeField] private Button projectSkill;
    [SerializeField] private Button healSkill;

    Button buttonName;

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private float lockTime = 2f;
    private Coroutine buttonDisabled = null;

    public List<Skill> skills = new List<Skill>();
    public List<string> sprites = new List<string>();

    public GameObject buttonSpawn;



    // Create a button depanding on what skills the player owns
    //  - In side the background 
    //  - List of skill to add buttons
    //  - Call a prefab button 
    //  - Work according to the skill
    //
    // Change the Game Manger to call the skill genericlay 

    // Spawn rate
    // Prepab 

    private void Start()
    {
        CreatButtons();
        
        foreach (Skill skill in skills)
        {
            String naming = skill.buttonSprite.name;
            buttonName = GameObject.Find(naming).GetComponent<Button>();
            buttonName.onClick.AddListener(ClickButton);
            buttonName.onClick.AddListener(() => _battleSceneCtrl.SkillCast(skill.name));
        }
    }
    public void ClickButton()
    {
        buttonDisabled = StartCoroutine(DisableButtonForSeconds(lockTime));
    }
    private IEnumerator DisableButtonForSeconds(float seconds)
    {
        print("Rin Through");
        foreach (Skill skill in skills)
        {
            String naming = skill.buttonSprite.name;
            GameObject.Find(naming).GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(seconds);

        foreach (Skill skill in skills)
        {
            String naming = skill.buttonSprite.name;
            GameObject.Find(naming).GetComponent<Button>().interactable = true;
        }

        buttonDisabled = null;
        
    }
    private void CreatButtons()
    {
        
        Transform canvas = this.transform.GetChild(0);

        if (skills.Count > 0 )
        {
            foreach ( Skill skill in skills ) 
            {
                buttonSpawn = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);
                
                buttonSpawn.gameObject.GetComponent<Image>().sprite=skill.buttonSprite;

                buttonSpawn.name = skill.buttonSprite.name;

                buttonSpawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill.name;

                RectTransform buttonTrans = buttonSpawn.GetComponent<RectTransform>();

                buttonTrans.transform.SetParent(canvas.transform);

            }

        }

        
    }
}
