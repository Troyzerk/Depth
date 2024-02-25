using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TownInfo : MonoBehaviour
{
    public Town town;
    public TownStats townStats;
    public GameObject townText;

    void Awake()
    {
        town = TownGenerator.GenerateTownStats(townStats, this.gameObject);
    }

    private void Start()
    {

        townText.GetComponent<TMP_Text>().text = town.townName;
    }
}
