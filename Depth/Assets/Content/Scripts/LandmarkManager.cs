using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LandmarkManager : MonoBehaviour
{

    public Landmark landmark;

    [SerializeField]
    Sprite goldSprite, expSprite, recruitSprite;


    private void Awake()
    {
        landmark = new();
        SolveReward();
    }


    void SolveReward()
    {
        //Define reward type.
        landmark.rewardType = (LandmarkRewardType)Random.Range(0, 3);

        //Solve Reward for the type of reward.
        if (landmark.rewardType == LandmarkRewardType.Gold)
        {
            landmark.rewardAmount = 50;
            landmark.rewardSprite = goldSprite;
        }
        
        if(landmark.rewardType == LandmarkRewardType.Experience)
        {
            landmark.rewardAmount = 100;
            landmark.rewardSprite = expSprite;
        }
        
        if (landmark.rewardType == LandmarkRewardType.NewRecruit)
        {
            landmark.rewardSprite = recruitSprite;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = landmark.rewardSprite;
    }
}



