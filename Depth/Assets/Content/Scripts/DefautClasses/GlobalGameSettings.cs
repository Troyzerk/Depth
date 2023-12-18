using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalGameSettings
{
    public static void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }
}
