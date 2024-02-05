using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUiScript : MonoBehaviour
{
    [SerializeField] private Button fireSkill;
    [SerializeField] private Button projectSkill;
    [SerializeField] private Button healSkill;

    bool buttonLocked = false;
    private float lockTime = 2f;
    private Coroutine buttonDisabled = null;

    private void Start()
    {
        fireSkill.onClick.AddListener(ClickButton);
        projectSkill.onClick.AddListener(ClickButton);
        healSkill.onClick.AddListener(ClickButton);
    }
    public void ClickButton()
    {
        buttonDisabled = StartCoroutine(DisableButtonForSeconds(lockTime));
    }
    private IEnumerator DisableButtonForSeconds(float seconds)
    {
        fireSkill.interactable = false;
        projectSkill.interactable = false;
        healSkill.interactable = false;
        yield return new WaitForSeconds(seconds);

        fireSkill.interactable = true;
        projectSkill.interactable = true;
        healSkill.interactable = true;

        buttonDisabled = null;
    }
}
