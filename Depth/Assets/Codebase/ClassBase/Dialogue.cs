using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : ScriptableObject
{
    public List<string> dialogueLines;
    public bool seen = false;
}

public class TownDialogue : Dialogue 
{
    public void StartDialogue()
    {
        if (!seen)
        {
            DialogueSystem dialogueSystem = new DialogueSystem();
            dialogueSystem.dialogueLines = dialogueLines;
        }
    }
    
    public  void EndDialogue()
    {
        seen = true;
        GameObject dialogueSystem = GameObject.FindGameObjectWithTag("DialogueSystem");
    }
}
