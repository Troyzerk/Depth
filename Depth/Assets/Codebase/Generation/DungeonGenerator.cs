using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DungeonGenerator
{
    public void InitDungeon()
    {
        LevelData newLevel = new LevelData();
        newLevel.levelName = "Dungeon_" + (PersistentManager.instance.dungeon.Count + 1);
        newLevel.dungeonNumber = PersistentManager.instance.dungeon.Count + 1;
        PersistentManager.instance.dungeon.Add(newLevel);
    }
    public LevelData GenerateDungeon(int dungeonNumber)
    {
        // Calculate random offsets based on dungeon number and seed
        float offsetX = CalculateOffset(dungeonNumber, 0);
        float offsetY = CalculateOffset(dungeonNumber, 1);

        LevelData dungeon = new LevelData();

        // Generate dungeon layout using seed and offsets
        // Use procedural algorithms and random numbers to create the dungeon's structure.

        return dungeon;
    }

    private float CalculateOffset(int dungeonNumber, int dimension)
    {
        // Use a deterministic function to calculate offset based on dungeon number and dimension
        // This ensures consistency across different runs with the same seed
        return (float)(dungeonNumber * 10 + dimension) / 100; // Adjust as needed
    }
}
