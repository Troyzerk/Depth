using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContainerManager : MonoBehaviour
{
    //vars
    GameObject heroStatCard;
    
    private void Awake()
    {
        if(heroStatCard == null)
        {
            heroStatCard = GameObject.Find("HUD/HeroContainer/StatCard");
        }
        // sets hero card as inactive on awake
        heroStatCard.SetActive(false);
    }
    public void StatButtonPress()
    {
       if (heroStatCard.activeInHierarchy == true)
        {
            heroStatCard.GetComponent<StatCardController>().DisableWindow();
        }
       else 
        {
            heroStatCard.GetComponent<StatCardController>().EnableWindow(PersistentManager.instance.playerCharacter);
        }
    }
}
