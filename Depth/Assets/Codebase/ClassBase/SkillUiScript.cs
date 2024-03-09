using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class SkillUiScript : MonoBehaviour
{
    [SerializeField] private BattleSceneCtrl _battleSceneCtrl;

    public List<Skill> skills = new List<Skill>();

    public GameObject buttonSpawn;

    private void Start()
    {
        CreatButtons();
    }
    private void CreatButtons()
    {
        
        Transform canvas = this.transform.GetChild(0);
        foreach (Skill skill in skills ) 
        {
            GameObject buttonOutput = Instantiate(buttonSpawn, Vector3.zero, Quaternion.identity);

            buttonOutput.gameObject.GetComponent<button_TypeCast>().skill = skill;

            Button thisButton = buttonOutput.transform.GetChild(0).GetComponent<Button>();
            thisButton.GetComponent<Image>().sprite = skill.buttonSprite;

            thisButton.GetComponentInChildren<TextMeshProUGUI>().text = skill.skillName;

            buttonOutput.name = skill.skillName;

            RectTransform buttonTrans = buttonOutput.GetComponent<RectTransform>();

            buttonTrans.transform.SetParent(canvas.transform);
        }

    }
}
