using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings 
{
    public string buildingName;
    public string buildingDesc;
    public string[] dialogue;
}

public class Church : Buildings
{    
    public void Interact()
    {
        DialogueSystem.instance.AddNewDialogue(dialogue, "Priest");
    }
}
