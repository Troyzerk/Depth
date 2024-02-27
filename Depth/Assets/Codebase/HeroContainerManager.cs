using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContainerManager : MonoBehaviour
{
    //vars
    public static HeroContainerManager instance { get; private set; }

    public GameObject statCard, heroImage, heroStatButton;

    private void Awake()
    {
        // sets hero card as inactive on awake
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        statCard.SetActive(false);
    }
    public void StatButtonPress()
    {
       if (statCard.activeInHierarchy == true)
        {
            statCard.GetComponent<StatCardController>().DisableWindow();
        }
       else 
        {
            statCard.GetComponent<StatCardController>().EnableWindow(PersistentManager.instance.playerCharacter);
        }
    }



}
