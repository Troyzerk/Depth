using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SubRaceStats", menuName = "Global/Races/SubRaceStats")]
public class SubRaceStats : ScriptableObject
{
    public RaceStats originRace;
    public SubRaceID subRaceID;

    // Requirements info. Should not be inherited.
    public string subRaceDescription;
    public string requirements;


    // Evolution bonuses when evolving
    public int speedModifier;
    public int healthModifier;
    public int manaModifier;
    public int damageModifier;
    public int defenceModifier;
    public DamageType damageTypeOverride;
    public Image portraitOverride;
}
