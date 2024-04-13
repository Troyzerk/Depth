using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public string levelName;
    public string levelID;
    public int dungeonNumber;
    public int floorAmount;

    public void InitLevelData()
    {
        levelID = $"Dungeon_{PersistentManager.instance.dungeon.Count + 1}";
        levelName = "Dungeon";
        floorAmount = Random.Range(1,3);
    }

}
