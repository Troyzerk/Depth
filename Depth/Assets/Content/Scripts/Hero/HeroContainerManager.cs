using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContainerManager : MonoBehaviour
{
    //vars
    public GameObject heroStatCard;
    
    private void Awake()
    {
        if(heroStatCard == null)
        {
            heroStatCard = GameObject.Find("HUD/HeroContainer/HeroStatCard");
            print("HeroStatCard found.");
        }
        // sets hero card as inactive on awake
        heroStatCard.SetActive(false);
    }
    public void StatButtonPress()
    {
       if (heroStatCard.activeInHierarchy == true)
        {
            heroStatCard.SetActive(false);
        }
       else 
        { 
            heroStatCard.SetActive(true);
        }
    }
}
