using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPriceList", menuName = "Price/Character/CharacterPriceList")]
public class CharacterPriceList : ScriptableObject
{
    public List<Object> characterPriceList = new();
}


