using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFaction", menuName = "Global/Faction")]
public class Faction : ScriptableObject
{
    public FactionID factionName;
    public List<FactionID> enemyFactions;
    public List<FactionID> friendlyFactions;
    public List<FactionID> neutralFactions;
}
