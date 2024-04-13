using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    public LevelData levelData;

    public void Start()
    {
        if(levelData == null)
        {
            levelData = new LevelData();
            levelData.InitLevelData();
        }
    }

    public void DescoverDungeon()
    {

    }
}
