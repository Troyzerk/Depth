using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorldRace : MonoBehaviour
{
    public string raceName;
    public string raceDisplayName;
    public List<WorldSkill> startingWorldSkills;
}


public class goblin : WorldRace
{
    public goblin()
    {
        raceName = "goblin";
        raceDisplayName = "Goblin";

        //starting race skills//
        //startingWorldSkills.Add(new smallStature());
        
    }

    public void addSkills()
    {
        //startingWorldSkills.Add(new Exist("Exist1", 1, 100, 0, false));
    }
}