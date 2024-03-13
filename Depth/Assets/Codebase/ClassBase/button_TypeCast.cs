using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_TypeCast : MonoBehaviour
{
    public BattleSceneCtrl _battleSceneCtrl;
    public Skill skill;

    [SerializeField] bool interactable = true;

    private void Awake()
    {
        interactable = true;
        Button buttonName = this.transform.GetChild(0).GetComponent<Button>();
        _battleSceneCtrl = GameObject.FindGameObjectWithTag("GameController").GetComponent<BattleSceneCtrl>();
        buttonName.onClick.AddListener(() => _battleSceneCtrl.SkillCast(skill.skillName));

    }
    public void ButtonPress()
    {
        if (interactable) 
        { 
            interactable = false;
            ClickButton();
        }
    }
    public void ClickButton()
    {
        StartCoroutine(DisableButtonForSeconds(skill.cooldown));

    }
    private IEnumerator DisableButtonForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        interactable = true;

    }

}
