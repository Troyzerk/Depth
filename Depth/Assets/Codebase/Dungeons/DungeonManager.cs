using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerData.instance.playerPartyObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
