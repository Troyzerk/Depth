using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatCardController : MonoBehaviour
{


    public void SetCharacterCardStats(Character character)
    {
        var nameInput = transform.Find("CardBase/Name").GetComponent<TMP_Text>();
        nameInput.text = character.characterFullName;

        var raceInput = transform.Find("CardBase/Race").GetComponent<TMP_Text>();
        raceInput.text = character.race.ToString() + " - " + character.subRace.ToString();

        var damageTypeInput = transform.Find("CardBase/DamageType").GetComponent<TMP_Text>();
        damageTypeInput.text = character.damageType.ToString();

        var titleInput = transform.Find("CardBase/Title").GetComponent<TMP_Text>();
        titleInput.text = character.title;

        var ageInput = transform.Find("CardBase/Stats/Age/AgeInput").GetComponent<TMP_Text>();
        ageInput.text = character.age.ToString();

        var healthInput = transform.Find("CardBase/Stats/Health/HealthInput").GetComponent<TMP_Text>();
        healthInput.text = character.currentHealth.ToString() + "/" + character.health.ToString();

        var manaInput = transform.Find("CardBase/Stats/Mana/ManaInput").GetComponent<TMP_Text>();
        manaInput.text = character.mana.ToString();

        var damageInput = transform.Find("CardBase/Stats/Damage/DamageInput").GetComponent<TMP_Text>();
        damageInput.text = character.damage.ToString();

        var defenceInput = transform.Find("CardBase/Stats/Defence/DefenceInput").GetComponent<TMP_Text>();
        defenceInput.text = character.defence.ToString();

        var speedInput = transform.Find("CardBase/Stats/Speed/SpeedInput").GetComponent<TMP_Text>();
        speedInput.text = character.speed.ToString();

    }

    public void DisableWindow()
    {
        gameObject.SetActive(false);
    }

    public void EnableWindow(Character character)
    {
        SetCharacterCardStats(character);
        gameObject.SetActive(true);

    }
}
