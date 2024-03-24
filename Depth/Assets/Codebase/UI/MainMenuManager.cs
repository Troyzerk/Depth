using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField seedInputText; 
    public static int seed;

    public void NewGame()
    {
        CheckSeed();
        SceneManager.LoadScene("LevelTest");
    }

    public void Tutorial()
    {
        CheckSeed();
        SceneManager.LoadScene("Tutorial");
    }

    public void CheckSeed()
    {

        string inputText = seedInputText.text.Trim();
        int parsedSeed;

        if (int.TryParse(inputText, out parsedSeed))
        {
            seed = parsedSeed;
        }
    }
}
