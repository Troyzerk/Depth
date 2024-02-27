using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDGlobalStats : MonoBehaviour
{
    public static HUDGlobalStats instance;

    [SerializeField]
    public GameObject 
        
        GoldText,
        GoldCounterText,
        RepText,
        RepCounterText,
        PartySpeedText,
        PartySpeedCounterText,
        PartyDamage,
        PartyDamageCounterText,
        PartyDefence,
        PartyDefenceCounterText;
}
