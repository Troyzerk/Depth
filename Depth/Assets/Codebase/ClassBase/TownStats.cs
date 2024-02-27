using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TownStats", menuName = "Gobal/TownStats")]
public class TownStats : ScriptableObject
{
    public RaceID owningRaceID;

    public string description;

    public List<string> townNames = new List<string>();
    public List<Sprite> portraits = new List<Sprite>();
    public List<DamageType> owningRaceDamageTypes = new List<DamageType>();

    public int maxStartingAge;
    public int minStartingAge;

    public int maxStartingDefence;
    public int minStartingDefence;
}

