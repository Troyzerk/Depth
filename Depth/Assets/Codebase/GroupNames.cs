using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Group info that are predefined in editor
 * there might be a better way of going about recording this
 * might be better to have these in .csv files and then pull from them.
 */

public static class GroupNames
{
    //public List<string> groupNames;
    public static List<string> AvailableGroupNames = new();

    public static void InitGroupNames()
    {
        AvailableGroupNames.Add("Wizards");
        AvailableGroupNames.Add("Harlets");
        AvailableGroupNames.Add("Bundle Of Sticks");
        AvailableGroupNames.Add("Neverend");
        AvailableGroupNames.Add("Waystrike");
        AvailableGroupNames.Add("Playtime");
        AvailableGroupNames.Add("SlipnSlide");
        AvailableGroupNames.Add("Darkway");
        AvailableGroupNames.Add("Brightway");
        AvailableGroupNames.Add("Neverway");
        AvailableGroupNames.Add("Wizway");
        AvailableGroupNames.Add("Crackles");
        AvailableGroupNames.Add("Leaf Bush");
        AvailableGroupNames.Add("Bandits");
        AvailableGroupNames.Add("Wild Bandits");
        AvailableGroupNames.Add("Sword and Shield");
        AvailableGroupNames.Add("Meat Dish");
        AvailableGroupNames.Add("Heart Charge");
        AvailableGroupNames.Add("Iron Hilt");
        AvailableGroupNames.Add("Still Water");

    }
}