using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownInfo : MonoBehaviour
{
    public Town town;
    public TownStats townStats;
    public GameObject townText;
    public bool isHordeDen = false;

    void Awake()
    {
        town = TownGenerator.GenerateTownStats(townStats, this.gameObject);
    }

    private void Start()
    {
        townText.GetComponent<TMP_Text>().text = town.townName;
        if (isHordeDen)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}
