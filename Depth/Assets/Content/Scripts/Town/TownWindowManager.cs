using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TownWindowManager : MonoBehaviour
{
    public Town town;

    public void Start()
    {

        GameObject.Find("TownName").GetComponent<TMP_Text>().text = town.name;
        GameObject.Find("TownDescription").GetComponent<TMP_Text>().text = town.description;
        GameObject.Find("TownAge").GetComponent<TMP_Text>().text = "Age : " + town.age.ToString();
        GameObject.Find("TownDefence").GetComponent<TMP_Text>().text = "Defence : " + town.defence;
        GameObject.Find("TownPortrait").GetComponent<Image>().sprite = town.portrait;
    }

}
