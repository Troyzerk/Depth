using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonWorldGenerator : BaseWorldGenerator
{
    public override void Start()
    {
        if (debugWorldGen)
        {
            CreateWorld();
        }
        base.Start();


        Transform startPos = GameObject.FindGameObjectWithTag("StartPos").transform;
        PlayerData.instance.playerPartyObject.transform.position = startPos.position;
        PlayerData.instance.playerPartyObject.SetActive(true);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void CreateWorld()
    {
        base.CreateWorld();
        SpawnSomething(1, Resources.Load("Gameplay/DungeonEntrance") as GameObject, null , nonblockingTileDatas);

    }


    
}
