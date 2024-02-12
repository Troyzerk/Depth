using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPartySpawner : MonoBehaviour
{
    public static void SpawnNPCGroups(int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject newNPCGroup = Instantiate(Resources.Load("Units/AIGroup") as GameObject,GameObject.Find("PersistantManager/AIGroups").transform);
            newNPCGroup.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }

    public static void SpawnTowns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newtown = Instantiate(Resources.Load("Town") as GameObject, GameObject.Find("PersistantManager/Towns").transform);
            newtown.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }

    public static void SpawnLandmark(int count)
    {
        for (int i = 0; i < count; i++)
        {
            //GameObject newLandmark = Instantiate(Resources.Load("Landmark") as GameObject, GameObject.Find("PersistantManager/Landmark").transform);
            //newLandmark.transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        }
    }
}
