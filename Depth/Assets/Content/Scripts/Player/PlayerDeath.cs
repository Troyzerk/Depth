using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    PlayerSkills playerSkills;

    private void Start()
    {
        playerSkills = GameObject.FindWithTag("PlayerController").GetComponent<PlayerSkills>();
    }

    public void killPlayer()
    {
        Debug.Log("Execute Player Death");
        playerSkills.ClearPlayerSkills();
    }

}
