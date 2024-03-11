using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class deathCounter_Ctrl : MonoBehaviour
{
    private GameObject DamageCounter;

    public void Counter()
    {
        int childCount = this.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy)
            {
                DamageCounter=this.transform.GetChild(i).gameObject;
                if (i ==  childCount)
                {
                    i = 0;
                }
            }
        }
    }
    public void SpawnCounter(GameObject target, int num, Color32 color)
    {
        print($"{target} {color}");
        Counter();
        DamageCounter.SetActive(true);
        DamageCounter.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().SetText(num.ToString());
        DamageCounter.transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>().color = color;
        DamageCounter.transform.GetComponent<DeathCounterAnim>().Attack();

    }

}
