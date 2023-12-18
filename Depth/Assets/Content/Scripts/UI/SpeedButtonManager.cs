using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedButtonManager : MonoBehaviour
{
    public GameState gameState;
    public void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameState>();
    }

    //Game Speeds
    public void PauseGameSpeed()
    {
        GlobalGameSettings.SetGameSpeed(0);
    }
    public void HalfSpeed()
    {
        GlobalGameSettings.SetGameSpeed(0.5f);
    }
    public void NormalSpeed()
    {
        GlobalGameSettings.SetGameSpeed(1);
    }
    public void DoubleSpeed()
    {
        GlobalGameSettings.SetGameSpeed(2);
    }
}
