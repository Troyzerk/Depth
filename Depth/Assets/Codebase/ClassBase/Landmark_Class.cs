using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Landmark
{
    public LandmarkRewardType rewardType;
    public int rewardAmount;
    public Sprite rewardSprite;
}

public enum LandmarkRewardType
{
    Gold,
    Experience,
    NewRecruit,
}