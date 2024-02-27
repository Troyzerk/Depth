using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownStatsGeneration : MonoBehaviour
{
    public Town town;
    public TownStats townStats;

    // Start is called before the first frame update
    void Awake()
    {
        town = TownGenerator.GenerateTownStats(townStats,this.gameObject);
    }
}
