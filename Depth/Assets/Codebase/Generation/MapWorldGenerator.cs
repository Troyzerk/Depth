using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapWorldGenerator : BaseWorldGenerator
{
    public override void Start()
    {
        if (debugWorldGen)
        {
            CreateWorld();
        }
        base.Start();
    }
    
    public override void Update()
    {
        base.Update();
    }

    public override void CreateWorld()
    {
        base.CreateWorld();
        SpawnGameplayObjects();
    }


    public void SpawnGameplayObjects()
    {
        //Spawn things
        NPCPartySpawner.SpawnNPCGroups(10);
        NPCPartySpawner.SpawnLandmark(20);
        SpawnSomething(5, Resources.Load("Gameplay/Town") as GameObject, GameObject.Find("PersistentManager/Towns").transform, nonblockingTileDatas);
        SpawnSomething(1, Resources.Load("Gameplay/HordeDen") as GameObject, GameObject.Find("PersistentManager/Towns").transform, nonblockingTileDatas);
        SpawnSomething(1, Resources.Load("Gameplay/DungeonEntrance") as GameObject, this.gameObject.transform, nonblockingTileDatas);
    }


    /*
     * 
     * 
     *
     * ------------------------------------------------------- Spawning Types -----------------------------------------------------------
     * 
     * 
     * 
     */
    public void SpawnTowns(int count)
    {
        for (int i = 0; i < count;)
        {
            TileData randomTile = blockingTileDatas[Random.Range(0, blockingTileDatas.Count)];

            if (randomTile.spawnOccupied == false)
            {
                GameObject newtown = Instantiate(Resources.Load("Town") as GameObject, GameObject.Find("PersistantManager/Towns").transform);

                newtown.gameObject.transform.position = randomTile.position;
                randomTile.spawnOccupied = true;
                print(newtown.name + " : Spawned at : " + randomTile.position);
                i++;
            }
        }
    }
}
